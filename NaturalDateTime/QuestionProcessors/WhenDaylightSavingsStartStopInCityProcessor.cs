using NodaTime;
using NodaTime.TimeZones;
using System;
using System.Text;

namespace NaturalDateTime
{
	public class WhenDaylightSavingInCityProcessor : QuestionProcessorBase, IQuestionProcessor
	{
		public bool CanAnswerQuestion(Question question)
		{
            return  question.Contains(DaylightSavings, City);
		}
		
		public Answer GetAnswer(Question question)
		{
            question.ResolveTokenValues();
            var cityToken = question.GetToken<CityToken>();
            var dateToken = question.GetToken<DateToken>();
            int? year = null;
            if (dateToken != null) year = dateToken.Year;
            DaylightSavingInfo daylightSavingInfo;
            if (year.HasValue)
            {
                var firstDateInTheYear = new LocalDateTime(year.Value, 1, 1, 0, 0).InZone(DateTimeZoneProviders.Tzdb[cityToken.City.Timezone], Resolvers.LenientResolver);
                var firstZoneIntervalInTheYear = firstDateInTheYear.GetZoneInterval();
                if (firstZoneIntervalInTheYear.IsoLocalEnd.Year > 10000) {
                    daylightSavingInfo = DaylightSavingInfo.CreateWithNoDaylightSavings();
                }
                else
                {
                    var firstDateInTheNextZoneInterval = firstDateInTheYear.Plus(firstZoneIntervalInTheYear.Duration).Plus(Duration.FromMilliseconds(1));
                    daylightSavingInfo = GetDaylightSavingInfo(firstDateInTheNextZoneInterval);
                }
            }
            else
            {
                daylightSavingInfo = GetDaylightSavingInfo(cityToken.GetCurrentTime());
            }
            var answerText = GetFormattedDaylightSavingInfo(daylightSavingInfo, cityToken.City);
            return new Answer(question, true, true, answerText);
		}

        public string GetFormattedDaylightSavingInfo(DaylightSavingInfo daylightSavingInfo, City city)
        {
            var formattedText = new StringBuilder();
            if (daylightSavingInfo.NoDaylightSavings)
            {
                formattedText.AppendFormat("Daylight saving time is not observed in {0}.", city.FormattedName);
            }
            else if(daylightSavingInfo.IsCurrentDaylightSavingInfo)
            {
                if(daylightSavingInfo.IsInDaylightSavingsTime)
                    formattedText.AppendFormat("{0} is currently in daylight saving time. It started on {1} when clocks were put forward by {2} and will end on {3} when clocks will be put back by {4}.", 
                        city.FormattedName, 
                        daylightSavingInfo.Start.GetFormattedDateAndTime(),
                        daylightSavingInfo.FormattedStartDateSavingPutForwardInHours,
                        daylightSavingInfo.End.GetFormattedDateAndTime(),
                        daylightSavingInfo.FormattedEndDateSavingPutBackInHours);
                else
                    formattedText.AppendFormat("{0} is currently not in daylight saving time. It ended on {1} when clocks were put back by {2} and will start again on {3} when clocks will be put forward by {4}.",
                        city.FormattedName,
                        daylightSavingInfo.End.GetFormattedDateAndTime(),
                        daylightSavingInfo.FormattedEndDateSavingPutBackInHours,
                        daylightSavingInfo.Start.GetFormattedDateAndTime(),
                        daylightSavingInfo.FormattedStartDateSavingPutForwardInHours);
            }
            else
            {
                var startOrStarted = "starts";
                var willBeOrWasPutForward = "will be";
                if (daylightSavingInfo.HasStarted)
                {
                    startOrStarted = "started";
                    willBeOrWasPutForward = "were";
                }
                var endOrEnded = "will end";
                var willBeOrWasPutBack = "will be";
                if (daylightSavingInfo.HasEnded)
                {
                    endOrEnded = "ended";
                    willBeOrWasPutBack = "were";
                }

                if(daylightSavingInfo.IsInDaylightSavingsTime)
                {
                    formattedText.AppendFormat("Daylight saving time {0} in {1} on {2} when clocks {3} put forward by {4} and {5} on {6} when clocks {7} put back by {8}.",
                        startOrStarted,
                        city.FormattedName,
                        daylightSavingInfo.Start.GetFormattedDateAndTime(),
                        willBeOrWasPutBack,
                        daylightSavingInfo.FormattedEndDateSavingPutBackInHours,
                        endOrEnded,
                        daylightSavingInfo.End.GetFormattedDateAndTime(),
                        willBeOrWasPutForward,
                        daylightSavingInfo.FormattedEndDateSavingPutBackInHours);
                }
                else
                {
                    formattedText.AppendFormat("Daylight saving time {0} in {1} on {2} when clocks {3} put back by {4} and {5} on {6} when clocks {7} put forward by {8}.",
                        endOrEnded,
                        city.FormattedName,
                        daylightSavingInfo.End.GetFormattedDateAndTime(),
                        willBeOrWasPutForward,
                        daylightSavingInfo.FormattedEndDateSavingPutBackInHours,
                        startOrStarted,
                        daylightSavingInfo.Start.GetFormattedDateAndTime(),
                        willBeOrWasPutBack,
                        daylightSavingInfo.FormattedEndDateSavingPutBackInHours);
                }
            }
            return formattedText.ToString();
        }

        private DaylightSavingInfo GetDaylightSavingInfo(ZonedDateTime zonedDateTime)
        {
            if (zonedDateTime.GetZoneInterval().IsoLocalEnd.Year > 10000) return DaylightSavingInfo.CreateWithNoDaylightSavings();

            var daylightSavingInfo = new DaylightSavingInfo { IsInDaylightSavingsTime = zonedDateTime.GetZoneInterval().Savings.Milliseconds > 0 };

            if (daylightSavingInfo.IsInDaylightSavingsTime)
            {
                daylightSavingInfo.End = zonedDateTime.GetZoneInterval().IsoLocalEnd;
                daylightSavingInfo.EndDateSavingPutBackInMilliseconds = zonedDateTime.GetZoneInterval().Savings.Milliseconds;
                daylightSavingInfo.HasEnded = daylightSavingInfo.End.ToDateTimeUnspecified() < zonedDateTime.LocalDateTime.ToDateTimeUnspecified();
                daylightSavingInfo.Start = zonedDateTime.GetZoneInterval().IsoLocalStart.Minus(Period.FromMilliseconds(zonedDateTime.GetZoneInterval().Savings.Milliseconds));
                daylightSavingInfo.StartDateSavingPutForwardInMilliseconds = zonedDateTime.GetZoneInterval().Savings.Milliseconds;
                daylightSavingInfo.HasStarted = daylightSavingInfo.Start.ToDateTimeUnspecified() < zonedDateTime.LocalDateTime.ToDateTimeUnspecified();
            }
            else
            {
                var previousZoneInterval = zonedDateTime.GetZoneInterval().Start.Minus(Duration.FromMilliseconds(1)).InZone(zonedDateTime.Zone).GetZoneInterval();
                daylightSavingInfo.End = previousZoneInterval.IsoLocalEnd;
                daylightSavingInfo.EndDateSavingPutBackInMilliseconds = previousZoneInterval.Savings.Milliseconds;
                daylightSavingInfo.HasEnded = daylightSavingInfo.End.ToDateTimeUnspecified() < zonedDateTime.LocalDateTime.ToDateTimeUnspecified();
                if (!daylightSavingInfo.NoDaylightSavings)
                {
                    var nextZoneInterval = zonedDateTime.GetZoneInterval().End.Plus(Duration.FromMilliseconds(1)).InZone(zonedDateTime.Zone).GetZoneInterval();
                    daylightSavingInfo.Start = nextZoneInterval.IsoLocalStart.Minus(Period.FromMilliseconds(nextZoneInterval.Savings.Milliseconds));
                    daylightSavingInfo.StartDateSavingPutForwardInMilliseconds = nextZoneInterval.Savings.Milliseconds;
                    daylightSavingInfo.HasStarted = daylightSavingInfo.Start.ToDateTimeUnspecified() < zonedDateTime.LocalDateTime.ToDateTimeUnspecified();
                }
            }

            return daylightSavingInfo;
        }
    }
}
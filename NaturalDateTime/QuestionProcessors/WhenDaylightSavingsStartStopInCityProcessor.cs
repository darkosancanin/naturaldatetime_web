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
            var cityToken = question.GetToken<CityToken>();
            var dateToken = question.GetToken<DateToken>();
            int? year = null;
            if (dateToken != null) year = dateToken.Year;
            var cityResolver = new CityResolver();
            var dateTimeManager = new DateTimeManager();
            var cityResolverResult = cityResolver.Resolve(cityToken);

            if (cityResolverResult.Status == CityResolverResultStatus.FAILED)
                return new Answer(question, true, false, ErrorMessages.UnableToRecognizeCity);
			
			if (cityResolverResult.City.HasNoTimezone)
                return new Answer(question, true, false, ErrorMessages.NoTimezone);

            DaylightSavingInfo daylightSavingInfo;
            if (year.HasValue)
                daylightSavingInfo = dateTimeManager.GetDaylightSavingInfo(year.Value, cityResolverResult.City.Timezone);
            else
                daylightSavingInfo = dateTimeManager.GetCurrentDaylightSavingInfo(cityResolverResult.City.Timezone);
            var answerText = GetFormattedDaylightSavingInfo(daylightSavingInfo, cityResolverResult.City);
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
	}
}
using NaturalDateTime.Extensions;
using NodaTime;
using NodaTime.TimeZones;
using System;

namespace NaturalDateTime
{
	public class WhatTimeInCityWhenTimeInAnotherCityQuestionHandler : QuestionHandler, IQuestionHandler
	{
		public bool CanAnswerQuestion(Question question)
		{
            return question.Contains(DateOrTimeToken) && question.ContainsExactNumberOfMatches(CityOrTimezoneToken, 2); ;
		}
		
		public Answer GetAnswer(Question question)
		{
            question.ResolveTokenValues();
			if(question.ContainsTokensInFollowingOrder(DateOrTimeToken, CityOrTimezoneToken, CityOrTimezoneToken))
                return GetAnswerToWhenTimeInKnownCityWhatTimeInUnknownCity(question); 
		    else
                return GetAnswerToWhatTimeInUnknownCityWhenTimeInKnownCity(question);
        }

        private Answer GetAnswerToWhenTimeInKnownCityWhatTimeInUnknownCity(Question question)
        {
            var knownCityOrTimezoneToken = question.GetToken<CityOrTimezoneToken >();
            var knownTimeToken = question.GetToken<TimeToken>();
            var knownDateToken = question.GetToken<DateToken>();
            var unknownCityOrTimezoneToken = question.GetToken<CityOrTimezoneToken >(2);
            return GetAnswerToTimeConversionQuestion(question, knownDateToken, knownTimeToken, knownCityOrTimezoneToken, unknownCityOrTimezoneToken);
        }

        private Answer GetAnswerToWhatTimeInUnknownCityWhenTimeInKnownCity(Question question)
        {
            var unknownCityToken = question.GetToken<CityOrTimezoneToken >();
            var knownCityToken = question.GetToken<CityOrTimezoneToken >(2);
            var knownTimeToken = question.GetToken<TimeToken>();
            var knownDateToken = question.GetToken<DateToken>();
            return GetAnswerToTimeConversionQuestion(question, knownDateToken, knownTimeToken, knownCityToken, unknownCityToken);
        }

        private Answer GetAnswerToTimeConversionQuestion(Question question, DateToken knownDateToken, TimeToken knownTimeToken, CityOrTimezoneToken  knownCityOrTimezone, CityOrTimezoneToken  unknownCityOrTimezone)
		{
            var knownEntityOffsetDateTime = OffsetDateTimeExtensions.CreateUpdatedOffsetDateTimeFromTokens(knownCityOrTimezone.GetCurrentTimeAsOffsetDateTime(), knownDateToken, knownTimeToken);
            Instant knownCityOrTimezoneInstant;
            String note = null;
            if (knownCityOrTimezone.GetType() == typeof(CityToken))
            {
                var cityToken = (CityToken)knownCityOrTimezone;
                ZoneLocalMapping knownCityOrTimezoneZoneLocalMapping = DateTimeZoneProviders.Tzdb[cityToken.City.Timezone].MapLocal(knownEntityOffsetDateTime.LocalDateTime);
                if (knownCityOrTimezoneZoneLocalMapping.Count == 1)
                {
                    knownCityOrTimezoneInstant = knownCityOrTimezoneZoneLocalMapping.Single().ToInstant();
                }
                else if (knownCityOrTimezoneZoneLocalMapping.Count > 1)
                {
                    knownCityOrTimezoneInstant = knownCityOrTimezoneZoneLocalMapping.First().ToInstant();
                    note = String.Format("In {0} the time {1} occurs twice due to daylight saving time changes when clocks are put back, we are using the first occurance of that time.",
                        cityToken.City.FormattedName,
                        knownEntityOffsetDateTime.LocalDateTime.GetFormattedTimeAndDate());
                }
                else
                {
                    knownCityOrTimezoneInstant = knownCityOrTimezoneZoneLocalMapping.LateInterval.Start.WithOffset(knownCityOrTimezoneZoneLocalMapping.LateInterval.WallOffset).ToInstant();
                    note = String.Format("In {0} the time {1} does not occur due to daylight saving time changes when clocks are put forward, we are using the next valid time.",
                        cityToken.City.FormattedName,
                        knownEntityOffsetDateTime.LocalDateTime.GetFormattedTimeAndDate());
                }
            }
            else if (knownCityOrTimezone.GetType() == typeof(TimezoneToken))
            {
                var timezoneToken = (TimezoneToken)knownCityOrTimezone;
                knownCityOrTimezoneInstant = knownEntityOffsetDateTime.LocalDateTime.WithOffset(timezoneToken.Timezone.Offset).ToInstant();
            }
            else
            {
                throw new NotImplementedException();
            }

            var answerText = String.Format("It is {0} in {1} when it is {2} in {3}",
                                            unknownCityOrTimezone.GetLocalDateTime(knownCityOrTimezoneInstant).GetFormattedTimeAndDate(),
                                            unknownCityOrTimezone.GetFormattedNameAndTimezone(knownCityOrTimezoneInstant),
                                            knownEntityOffsetDateTime.LocalDateTime.GetFormattedTimeAndDate(),
                                            knownCityOrTimezone.GetFormattedNameAndTimezone(knownEntityOffsetDateTime.ToInstant()));

            var answer = new Answer(question, true, true, answerText);
            answer.Note = note;
            return answer;

		}
	}
}
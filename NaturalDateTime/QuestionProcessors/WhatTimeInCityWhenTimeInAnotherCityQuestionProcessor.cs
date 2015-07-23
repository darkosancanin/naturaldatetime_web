using NaturalDateTime.Extensions;
using NodaTime;
using NodaTime.TimeZones;
using System;

namespace NaturalDateTime
{
	public class WhatTimeInCityWhenTimeInAnotherCityQuestionProcessor : QuestionProcessorBase, IQuestionProcessor
	{
		public bool CanAnswerQuestion(Question question)
		{
            return question.Contains(DateOrTimeToken) && question.ContainsExactNumberOfMatches(TokenWithKnownOffset, 2); ;
		}
		
		public Answer GetAnswer(Question question)
		{
            question.ResolveTokenValues();
			if(question.ContainsTokensInFollowingOrder(DateOrTimeToken, TokenWithKnownOffset, TokenWithKnownOffset))
                return GetAnswerToWhenTimeInKnownCityWhatTimeInUnknownCity(question); 
		    else
                return GetAnswerToWhatTimeInUnknownCityWhenTimeInKnownCity(question);
        }

        private Answer GetAnswerToWhenTimeInKnownCityWhatTimeInUnknownCity(Question question)
        {
            var knownCityToken = question.GetToken<TokenWithKnownOffset>();
            var knownTimeToken = question.GetToken<TimeToken>();
            var knownDateToken = question.GetToken<DateToken>();
            var unknownCityToken = question.GetToken<TokenWithKnownOffset>(2);
            return GetAnswerToTimeConversionQuestion(question, knownDateToken, knownTimeToken, knownCityToken, unknownCityToken);
        }

        private Answer GetAnswerToWhatTimeInUnknownCityWhenTimeInKnownCity(Question question)
        {
            var unknownCityToken = question.GetToken<TokenWithKnownOffset>();
            var knownCityToken = question.GetToken<TokenWithKnownOffset>(2);
            var knownTimeToken = question.GetToken<TimeToken>();
            var knownDateToken = question.GetToken<DateToken>();
            return GetAnswerToTimeConversionQuestion(question, knownDateToken, knownTimeToken, knownCityToken, unknownCityToken);
        }

        private Answer GetAnswerToTimeConversionQuestion(Question question, DateToken knownDateToken, TimeToken knownTimeToken, TokenWithKnownOffset knownOffsetToken, TokenWithKnownOffset unknownOffsetToken)
		{
            var knownEntityOffsetDateTime = OffsetDateTimeExtensions.CreateUpdatedOffsetDateTimeFromTokens(knownOffsetToken.GetCurrentTimeAsOffsetDateTime(), knownDateToken, knownTimeToken);
            Instant knownEntityInstant;
            String note = null;
            if (knownOffsetToken.GetType() == typeof(CityToken))
            {
                var cityToken = (CityToken)knownOffsetToken;
                ZoneLocalMapping knownEntityZoneLocalMapping = DateTimeZoneProviders.Tzdb[cityToken.City.Timezone].MapLocal(knownEntityOffsetDateTime.LocalDateTime);
                if (knownEntityZoneLocalMapping.Count == 1)
                {
                    knownEntityInstant = knownEntityZoneLocalMapping.Single().ToInstant();
                }
                else if (knownEntityZoneLocalMapping.Count > 1)
                {
                    knownEntityInstant = knownEntityZoneLocalMapping.First().ToInstant();
                    note = String.Format("In {0} the time {1} occurs twice due to daylight saving time changes when clocks are put back, we are using the first occurance of that time.",
                        cityToken.City.FormattedName,
                        knownEntityOffsetDateTime.LocalDateTime.GetFormattedTimeAndDate());
                }
                else
                {
                    knownEntityInstant = knownEntityZoneLocalMapping.LateInterval.Start.WithOffset(knownEntityZoneLocalMapping.LateInterval.WallOffset).ToInstant();
                    note = String.Format("In {0} the time {1} does not occur due to daylight saving time changes when clocks are put forward, we are using the next valid time.",
                        cityToken.City.FormattedName,
                        knownEntityOffsetDateTime.LocalDateTime.GetFormattedTimeAndDate());
                }
            }
            else if (knownOffsetToken.GetType() == typeof(TimezoneToken))
            {
                var timezoneToken = (TimezoneToken)knownOffsetToken;
                knownEntityInstant = knownEntityOffsetDateTime.LocalDateTime.WithOffset(timezoneToken.Timezone.Offset).ToInstant();
            }
            else
            {
                throw new NotImplementedException();
            }

            var answerText = String.Format("It is {0} in {1} when it is {2} in {3}",
                                            unknownOffsetToken.GetLocalDateTime(knownEntityInstant).GetFormattedTimeAndDate(),
                                            unknownOffsetToken.GetFormattedNameAndTimezone(knownEntityInstant),
                                            knownEntityOffsetDateTime.LocalDateTime.GetFormattedTimeAndDate(),
                                            knownOffsetToken.GetFormattedNameAndTimezone(knownEntityOffsetDateTime.ToInstant()));

            var answer = new Answer(question, true, true, answerText);
            answer.Note = note;
            return answer;

		}
	}
}
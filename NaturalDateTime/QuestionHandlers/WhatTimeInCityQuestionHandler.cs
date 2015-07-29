using System;
using System.Linq;

namespace NaturalDateTime
{
	public class WhatTimeInCityQuestionHandler : QuestionHandler, IQuestionHandler
	{
		public bool CanAnswerQuestion(Question question)
		{
            return (question.Contains(LiteralDateOrTime) && question.ContainsExactNumberOfMatches(CityOrTimezoneToken, 1)) || (question.Contains(Timezone) && question.Tokens.Count == 1);
		}
		
		public Answer GetAnswer(Question question)
		{
			return GetAnswerToWhatTimeInCity(question);
		}

        private Answer GetAnswerToWhatTimeInCity(Question question)
		{
            CityOrTimezoneToken  cityOrTimezoneToken = question.GetToken<CityOrTimezoneToken >();
			var answerText = String.Format("The current time in {0} is {1}.",
                                            cityOrTimezoneToken.GetFormattedNameAndTimezone(cityOrTimezoneToken.GetCurrentTimeAsOffsetDateTime().ToInstant()),
                                            cityOrTimezoneToken.GetCurrentTimeAsOffsetDateTime().LocalDateTime.GetFormattedTimeAndDate());
			return new Answer(question, true, true, answerText);	
		}
	}
}
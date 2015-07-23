using System;
using System.Linq;

namespace NaturalDateTime
{
	public class WhatTimeInCityQuestionProcessor : QuestionProcessorBase, IQuestionProcessor
	{
		public bool CanAnswerQuestion(Question question)
		{
            return (question.Contains(LiteralDateOrTime) && question.ContainsExactNumberOfMatches(TokenWithKnownOffset, 1)) || (question.Contains(Timezone) && question.Tokens.Count == 1);
		}
		
		public Answer GetAnswer(Question question)
		{
            question.ResolveTokenValues();
			return GetAnswerToWhatTimeInCity(question);
		}

        private Answer GetAnswerToWhatTimeInCity(Question question)
		{
            TokenWithKnownOffset zonedToken = question.GetToken<TokenWithKnownOffset>();
			var answerText = String.Format("The current time in {0} is {1}.",
                                            zonedToken.GetFormattedNameAndTimezone(zonedToken.GetCurrentTimeAsOffsetDateTime().ToInstant()),
                                            zonedToken.GetCurrentTimeAsOffsetDateTime().LocalDateTime.GetFormattedTimeAndDate());
			return new Answer(question, true, true, answerText);	
		}
	}
}
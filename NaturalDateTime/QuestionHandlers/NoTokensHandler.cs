using System;

namespace NaturalDateTime
{
	public class NoTokensHandler : QuestionHandler, IQuestionHandler
	{
		public bool CanAnswerQuestion(Question question)
		{
            return question.Tokens.Count == 0;
		}
		
		public Answer GetAnswer(Question question)
		{
            CityToken cityToken = CityTokenizer.CreateCityTokenFromQuestionWithNoTokens(question);
            cityToken.ResolveTokenValues();
            var currentTime = cityToken.GetCurrentTimeAsOffsetDateTime();
            var answerText = String.Format("The current time in {0} is {1}.",
                                            cityToken.GetFormattedNameAndTimezone(currentTime.ToInstant()),
                                            currentTime.LocalDateTime.GetFormattedTimeAndDate());
            return new Answer(question, true, true, answerText);
        }

        public override bool UnderstoodQuestion
        {
            get { return false; }
        }
    }
}
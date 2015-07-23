using System;

namespace NaturalDateTime
{
	public interface IQuestionHandler
	{
		bool CanAnswerQuestion(Question question);
		Answer GetAnswer(Question question);
        bool UnderstoodQuestion { get; }

    }
}


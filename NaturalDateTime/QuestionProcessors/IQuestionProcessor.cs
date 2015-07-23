using System;

namespace NaturalDateTime
{
	public interface IQuestionProcessor
	{
		bool CanAnswerQuestion(Question question);
		Answer GetAnswer(Question question);
        bool UnderstoodQuestion { get; }

    }
}


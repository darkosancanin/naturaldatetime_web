using NaturalDateTime.Domain;
using System;
using System.Linq;
using System.Collections.Generic;

namespace NaturalDateTime
{
	public class Answer
	{
        public Question Question { get; set; }
        public string AnswerText { get; set; }
		public bool AnsweredQuestion { get;set; }
        public string Note { get; set; }
		public bool UnderstoodQuestion { get; set; }
        public IList<DebugInformation> DebugInformation { get; set; }

        public Answer(Question question, bool understoodQuestion, bool answeredQuestion, string answerText)
		{
			Question = question;
			AnswerText = answerText;
            AnsweredQuestion = answeredQuestion;
			UnderstoodQuestion = understoodQuestion;
            DebugInformation = new List<DebugInformation>();
        }

        public void AddDebugInformation(string name, string value)
        {
            DebugInformation.Add(new DebugInformation(name, value));
        }

        public void AddDebugInformation(IList<DebugInformation> debugInformation)
        {
            foreach (var di in debugInformation)
                DebugInformation.Add(di);
        }
    }
}


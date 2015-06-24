using System;
using System.Collections.Generic;

namespace NaturalDateTime
{
    public class EventTokenizer : BaseTokenizer
    {
		public override void TokenizeTheQuestion(Question question)
        {
            var listOfEvents = new [] { "easter", "christmas", "new years", @"new year\s", @"new year$" };
            var regexExpression = String.Join("|", listOfEvents);
            AddTokensToQuestion(question, regexExpression);
        }
    }
}

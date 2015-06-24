using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NaturalDateTime
{
    public class DaylightSavingsTokenizer : ITokenizer
    {
		public void TokenizeTheQuestion(Question question)
        {
            var matches = Regex.Matches(question.QuestionText, @"(day(\s)?light)\ssaving('s|s)?\s(time('s|s)?)?", RegexOptions.IgnoreCase);
            foreach (Match match in matches)
            {
                Group group = match.Groups[0];
                var token = new DaylightSavingsToken(group.Value, group.Index);
                question.AddToken(token);
            }
        }
    }
}

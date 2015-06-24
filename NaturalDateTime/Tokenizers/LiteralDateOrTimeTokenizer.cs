using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NaturalDateTime
{
    public class LiteralDateOrTimeTokenizer : ITokenizer
    {
		public void TokenizeTheQuestion(Question question)
        {
			var matches = Regex.Matches(question.QuestionText, @"(^|\s)(date|time)(\s|$)", RegexOptions.IgnoreCase);
            foreach (Match match in matches)
            {
                Group group = match.Groups[0];
                var token = new LiteralDateOrTimeToken(group.Value, group.Index);
                question.AddToken(token);
            }
        }
    }
}

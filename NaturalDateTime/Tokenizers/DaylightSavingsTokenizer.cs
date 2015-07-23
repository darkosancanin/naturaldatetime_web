using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NaturalDateTime
{
    public class DaylightSavingsTokenizer : ITokenizer
    {
		public void TokenizeTheQuestion(Question question)
        {
            var daylightSavingsTimeRegex = @"(day(\s)?light)\ssaving('s|s)?\s(time('s|s)?)?";
            var dstRegex = @"dst\s";
            var matches = Regex.Matches(question.QuestionText, string.Format(@"(^|\s)({0})|({1})", daylightSavingsTimeRegex, dstRegex), RegexOptions.IgnoreCase);
            foreach (Match match in matches)
            {
                Group group = match.Groups[0];
                var token = new DaylightSavingsToken(group.Value, group.Index);
                question.AddToken(token);
            }
        }
    }
}

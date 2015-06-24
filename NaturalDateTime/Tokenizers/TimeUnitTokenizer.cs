using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NaturalDateTime
{
    public class TimeUnitTokenizer : ITokenizer
    {
		public void TokenizeTheQuestion(Question question)
        {
            var periodsRegex = @"(day|days|day's|second|seconds|second's|minute|minutes|minute's|year|years|year's)";
			var matches = Regex.Matches(question.QuestionText, @"(^|\s)" + periodsRegex + @"(\s|$)", RegexOptions.IgnoreCase);
            foreach (Match match in matches)
            {
                Group group = match.Groups[0];
                var token = new TimeUnitToken(group.Value, group.Index);
                question.AddToken(token);
            }
        }
    }
}

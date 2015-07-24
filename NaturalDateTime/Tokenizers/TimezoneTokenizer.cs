using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NaturalDateTime
{
    public class TimezoneTokenizer: ITokenizer
    {
        public void TokenizeTheQuestion(Question question)
        {
            var timezoneRegex = new StringBuilder();
            timezoneRegex.Append("(");
            var isFirstIteration = true;
            foreach (var timezone in Timezones.GetAllTimezones())
            {
                if (!isFirstIteration)
                    timezoneRegex.Append("|");
                timezoneRegex.Append(String.Format("{0}|{1}", timezone.Name, timezone.Abbreviation));
                isFirstIteration = false;
            }
                
            timezoneRegex.Append(")");
            var matches = Regex.Matches(question.QuestionText, @"(^|\s)" + timezoneRegex.ToString() + @"(\s|$|,)", RegexOptions.IgnoreCase);
            foreach (Match match in matches)
            {
                Group group = match.Groups[0];
                var token = new TimezoneToken(group.Value, group.Index);
                question.AddToken(token);
            }
        }
    }
}

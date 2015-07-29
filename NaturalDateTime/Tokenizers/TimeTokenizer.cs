using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NaturalDateTime
{
    public class TimeTokenizer : ITokenizer
    {
        public void TokenizeTheQuestion(Question question)
        {
            var abbreviationTokens = GetAbbreviations(question);
            question.AddTokens(abbreviationTokens);
            if (abbreviationTokens.Count == 0)
            {
                var standardTimeFormatTokens = GetStandardTimeFormats(question);
                question.AddTokens(standardTimeFormatTokens);
            } 
        }

        private IList<Token> GetAbbreviations(Question question)
        {
            var tokens = new List<Token>();
            var matches = Regex.Matches(question.QuestionText, @"(^|\s)(?<abbreviation>(noon|mid(-|\s)?day|mid(-|\s)?night))(\s|$)", RegexOptions.IgnoreCase);
            foreach (Match match in matches)
            {
                Group abbreviationMatch = match.Groups["abbreviation"];
                if (abbreviationMatch.Value.EndsWith("noon") || abbreviationMatch.Value.EndsWith("day"))
                    tokens.Add(new TimeToken(abbreviationMatch.Value, abbreviationMatch.Index, 12, null, Meridiem.PM));
                else if (abbreviationMatch.Value.EndsWith("night"))
                    tokens.Add(new TimeToken(abbreviationMatch.Value, abbreviationMatch.Index, 12, null, Meridiem.AM));
            }
            return tokens;
        }

        private IList<Token> GetStandardTimeFormats(Question question)
        {
            var tokens = new List<Token>();
            var matches = Regex.Matches(question.QuestionText, @"(^|\s)(?<first_number>\d{1,2})((:|\.)(?<second_number>\d{1,2}))?\s?(?<meridiem_group>(\.?(a(\.)?m|p(\.)?m)\.?))?\s?(o(')?clock)?(\s|$)", RegexOptions.IgnoreCase);
            foreach (Match match in matches)
            {
                Group group = match.Groups[0];
                var firstNumber = match.Groups["first_number"];
                var secondNumber = match.Groups["second_number"];
                var meridiem_group = match.Groups["meridiem_group"];
                int hour = int.Parse(firstNumber.Value);
                int? minute = null;
                if (!string.IsNullOrEmpty(secondNumber.Value)) minute = int.Parse(secondNumber.Value);
                Meridiem meridiem = Meridiem.NONE;
                if (!string.IsNullOrEmpty(meridiem_group.Value))
                {
                    var meridiemValue = meridiem_group.Value.ToLower();
                    if (meridiemValue.Contains("a") && meridiemValue.Contains("m")) meridiem = Meridiem.AM;
                    else if (meridiemValue.Contains("p") && meridiemValue.Contains("m")) meridiem = Meridiem.PM;
                }

                tokens.Add(new TimeToken(group.Value, match.Index, hour, minute, meridiem));
            }
            return tokens;
        }
    }
}


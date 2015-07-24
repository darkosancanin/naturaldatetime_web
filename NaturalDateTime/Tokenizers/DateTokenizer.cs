using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace NaturalDateTime
{
    public class DateTokenizer : ITokenizer
    {
		public void TokenizeTheQuestion(Question question)
        {
            var dayMonthYearTokens = GetDayMonthYearFormattedDates(question);
            question.AddTokens(dayMonthYearTokens);
            if (dayMonthYearTokens.Count == 0)
            {
                var monthDayYearTokens = GetMonthDayYearFormattedDates(question);
                question.AddTokens(monthDayYearTokens);
                if (monthDayYearTokens.Count == 0)
                {
                    var hyphenAndSlashTokens = GetHyphenAndSlashFormattedDates(question);
                    question.AddTokens(hyphenAndSlashTokens);
                    if (hyphenAndSlashTokens.Count == 0)
                    {
                        question.AddTokens(GetYearOnlyDates(question));
                    }
                }
            }
        }

        private IList<Token> GetYearOnlyDates(Question question)
		{
			var tokens = new List<Token>();
            var matches = Regex.Matches(question.QuestionText, @"\s(?<year>\d{4})(\s|$)", RegexOptions.IgnoreCase);
			foreach (Match match in matches)
            {
                Group yearMatch = match.Groups["year"];
				var year = int.Parse (yearMatch.Value);
                var tokenResult = new DateToken(yearMatch.Value, yearMatch.Index, null, null, year);
                tokens.Add(tokenResult);
            }
			return tokens;
		}

        private IList<Token> GetDayMonthYearFormattedDates(Question question)
		{
			var tokens = new List<Token>();
			var monthNamesRegex = "(jan(uary)?|febr(uary)?|mar(ch)?|apr(il)?|may|jun(e)?|jul(y)?|aug(ust)?|sep(tember)?|oct(ober)?|nov(ember)?|dec(ember)?)";
            var matches = Regex.Matches(question.QuestionText, @"(?<day>\d{1,2})(th|st|nd|rd)?(\sof|/|-)?\s?(?<month_name>" + monthNamesRegex + @")((/|-|\s)(?<year>\d{2,4}))?", RegexOptions.IgnoreCase);
			foreach (Match match in matches)
            {
                Group group = match.Groups[0];
				var monthNameMatch = match.Groups["month_name"];
				var dayMatch = match.Groups["day"];
				var yearMatch = match.Groups["year"];
				
				var month = GetMonth(monthNameMatch.Value);
				var day = int.Parse(dayMatch.Value);
				int? year = null;
				if(!string.IsNullOrEmpty(yearMatch.Value)) year = int.Parse(yearMatch.Value);
				year = MakeYearInThisCenturyIfOnly2Digits(year);
                var tokenResult = new DateToken(group.Value, group.Index, day, month, year);
                tokens.Add(tokenResult);
            }
			return tokens;
		}
		
		public int? GetMonth(string monthNameMatch)
		{
			monthNameMatch = monthNameMatch.ToLower();
			if(monthNameMatch.StartsWith("jan")) return 1;
			else if(monthNameMatch.StartsWith("feb")) return 2;
			else if(monthNameMatch.StartsWith("mar")) return 3;
			else if(monthNameMatch.StartsWith("apr")) return 4;
			else if(monthNameMatch.StartsWith("may")) return 5;
			else if(monthNameMatch.StartsWith("jun")) return 6;
			else if(monthNameMatch.StartsWith("jul")) return 7;
			else if(monthNameMatch.StartsWith("aug")) return 8;
			else if(monthNameMatch.StartsWith("sep")) return 9;
			else if(monthNameMatch.StartsWith("oct")) return 10;
			else if(monthNameMatch.StartsWith("nov")) return 11;
			else if(monthNameMatch.StartsWith("dec")) return 12;
			return null; 
		}
		
		private IList<Token> GetMonthDayYearFormattedDates (Question question)
		{
			var tokens = new List<Token>();
			var monthNamesRegex = "(jan(uary)?|feb(ruary)?|mar(ch)?|apr(il)?|may|jun(e)?|jul(y)?|aug(ust)?|sep(tember)?|oct(ober)?|nov(ember)?|dec(ember)?)";
            var matches = Regex.Matches(question.QuestionText, @"(?<month_name>" + monthNamesRegex + @")(\sthe\s|,\s|,|\s)(?<first_number>\d{1,4})(th|st|nd|rd)?(?<second_number>\s?\d{4})?", RegexOptions.IgnoreCase);
			foreach (Match match in matches)
            {
                Group group = match.Groups[0];
				var monthNameMatch = match.Groups["month_name"];
				var firstNumberMatch = match.Groups["first_number"];
				var secondNumberMatch = match.Groups["second_number"];
				
				var month = GetMonth(monthNameMatch.Value);
				int? firstNumber = int.Parse (firstNumberMatch.Value);
				int? secondNumber = null;
				if(!string.IsNullOrEmpty (secondNumberMatch.Value)) secondNumber = int.Parse(secondNumberMatch.Value);	
				int? day = null;
				int? year = null;
				if(secondNumber.HasValue) 
				{
					day = firstNumber;
					year = secondNumber;
				}
				else
				{
					if(firstNumber <= 31){
						day = firstNumber;
					}
					else{
						year = firstNumber;
					}
				}
				year = MakeYearInThisCenturyIfOnly2Digits(year);
				var tokenResult = new DateToken(group.Value, monthNameMatch.Index, day, month, year);
                tokens.Add(tokenResult);
            }
			return tokens;
		}

        private IList<Token> GetHyphenAndSlashFormattedDates(Question question)
		{
			var tokens = new List<Token>();
            var matches = Regex.Matches(question.QuestionText, @"(?<first_number>\d{1,2})(-|/)(?<second_number>\d{1,2})(-|/)(?<year>\d{2,4})", RegexOptions.IgnoreCase);
			foreach (Match match in matches)
            {
                Group group = match.Groups[0];
				var yearMatch = match.Groups["year"];
				var firstNumberMatch = match.Groups["first_number"];
				var secondNumberMatch = match.Groups["second_number"];
				
				int? year = int.Parse(yearMatch.Value);
				var firstNumber = int.Parse(firstNumberMatch.Value);
				var secondNumber = int.Parse(secondNumberMatch.Value);
				int day = secondNumber;
				int month = firstNumber;
				if(firstNumber > 12 && secondNumber <= 12){
					day = firstNumber;
					month = secondNumber;
				}
				year = MakeYearInThisCenturyIfOnly2Digits(year);
                var tokenResult = new DateToken(group.Value, yearMatch.Index, day, month, year);
                tokens.Add(tokenResult);
            }
			return tokens;
		}

		private int? MakeYearInThisCenturyIfOnly2Digits (int? year)
		{
			if(year.HasValue && year.Value < 100){
				return year += 2000;
			}
			return year;
		}
    }
}


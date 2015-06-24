using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NaturalDateTime
{
    public class CityTokenizer: ITokenizer
    {
		public void TokenizeTheQuestion(Question question)
        {
            var matches = Regex.Matches(question.QuestionText, @"(^|\s)(in|at)\s(the\s)?", RegexOptions.IgnoreCase);
            
            foreach (Match match in matches)
            {
                Group group = match.Groups[0];
                var startPosition = (group.Index + group.Length );
                var cityName = question.QuestionText.Substring(startPosition);
                var endPosition = question.QuestionText.Length;
                
                var possibleEarlierTerminations = Regex.Matches(cityName, @"(^|\s)(when|what('?s?)|\d|(on|in|at|a|right|now|if|then|\?)(\s|$))", RegexOptions.IgnoreCase);
                if(possibleEarlierTerminations.Count > 0)
                {
                    Group terminationGroup = GetEarlierOccurrenceOfGroup(possibleEarlierTerminations);
                    endPosition = startPosition + terminationGroup.Index;
                }

                cityName = question.QuestionText.Substring(startPosition, endPosition - startPosition);
                if(cityName.Replace(" ","").Length == 0) continue;

                var tokenResult = new CityToken(cityName, startPosition);
                question.AddToken(tokenResult);
            }
        }

        private Group GetEarlierOccurrenceOfGroup(MatchCollection matchCollection)
        {
            Group earliestGroup = matchCollection[0];
            foreach (Match match in matchCollection)
            {
                Group group = match.Groups[0];
                if (group.Index < earliestGroup.Index)
                    earliestGroup = group;
            }
            return earliestGroup;
        }
		
		public static CityToken CreateCityTokenFromQuestionWithNoTokens(Question question){
			var cityName = question.QuestionText.Trim();
			return new CityToken(cityName, 0);	
		}
    }
}

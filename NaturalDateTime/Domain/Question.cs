using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NaturalDateTime
{
	public class Question
	{
		public string QuestionText { get; set; }
		public string PreProcessedQuestionText { get; set; }
		private IList<Token> _tokens = new List<Token>();

        public Question(string question)
        {
            PreProcessedQuestionText = question;
            QuestionText = PreProcessText(question);
            Tokenizers.GetAll().ForEach(x => x.TokenizeTheQuestion(this));
            OrderTokens();
        }

        public string PreProcessText(string text)
        {
			text = text.Trim().Trim('?').Trim();
            return Regex.Replace(text, @"\s+", " ", RegexOptions.Multiline);
        }
		
		public void AddToken(Token token){
			var overlappingTokens = _tokens.Where(x => x.Value.Contains(token.Value) && token.LengthOfMatch != x.LengthOfMatch).ToList();
			var hasLongerOverlappingTokenAlready = false;
			foreach(var overlappingToken in overlappingTokens){
				if(token.LengthOfMatch < overlappingToken.LengthOfMatch) {
					hasLongerOverlappingTokenAlready = true;
				}
				else{
					_tokens.Remove(overlappingToken);	
				}
			}
			if(!hasLongerOverlappingTokenAlready){
				_tokens.Add(token);
			}
		}
		
		public void AddTokens(IList<Token> tokens){
			foreach(var token in tokens){
				AddToken (token);
			}
		}
		
		public void OrderTokens(){
			_tokens = _tokens.OrderBy(x => x.Position).ToList();	
		}
		
		public void RemoveAllTokens(){
			_tokens.Clear();	
		}
		
		public IList<Token> Tokens 
		{
			get { return _tokens; }	
		}
		
		public string FormatTextWithTokens()
        {
            var formattedText = QuestionText;
			var currentOffset = 0;
            foreach (Token token in _tokens)
            {
                var startTag = token.FormattedStartTag;
                var endTag = token.FormattedEndTag;
                formattedText = formattedText.Insert(QuestionText.IndexOf(token.Value) + currentOffset, startTag);
                currentOffset += startTag.Length;
                formattedText = formattedText.Insert(QuestionText.IndexOf(token.Value) + token.LengthOfMatch + currentOffset, endTag);
                currentOffset += endTag.Length;
            }
            return formattedText;
        }
		
		public string FormatTokenStructure()
        {
            var formattedText = "";
            foreach (Token token in _tokens)
            {
                formattedText += token.GetType().Name.Replace("Token","") + " | ";
            }
            return formattedText.TrimEnd(new char[] {' ', '|'});
        }

		public bool ContainsTokensInFollowingOrder(params Type[] tokenNames)
        {
			var numberOfItemsToSkip = 0;
            foreach(var tokenType in tokenNames){
				var matchingToken = _tokens.Skip(numberOfItemsToSkip).Where(x => x.GetType() == tokenType).FirstOrDefault();
				if(matchingToken == null) return false;
				numberOfItemsToSkip = _tokens.IndexOf(matchingToken) + 1;
			}
			return true;
        }
		
		public bool ContainsMultipleOccurrences(Type tokenType, int numberOfOccurrences)
        {
            return _tokens.Where(x => x.GetType() == tokenType).Count() == numberOfOccurrences;
        }

        public bool Contains(params Type[] tokenTypes)
        {
            var tokens = _tokens.ToList();
            if (tokenTypes.Any(tokenType => !tokens.Exists(x => x.GetType() == tokenType)))
                return false;

            return true;
        }
		
		public bool ContainsAnyOfTheFollowing(params Type[] tokenTypes)
        {
            var tokens = _tokens.ToList();
            if (tokenTypes.Any(tokenType => tokens.Exists(x => x.GetType() == tokenType)))
                return true;

            return false;
        }
		
		public bool MatchesStructure(params Type[] tokenTypes){
			if(_tokens.Count >= tokenTypes.Count()){
				for(int x = 0; x < tokenTypes.Count(); x++){
					if(_tokens[x].GetType() != tokenTypes[x]) return false;
				}
				return true;
			}
			
			return false;
		}

		public T GetToken<T>(int whichOccuranceOfToken) where T : Token {
			return (T)_tokens.Where(x => x is T).Skip(whichOccuranceOfToken - 1).FirstOrDefault();
		}
		
		public T GetToken<T>() where T : Token {
			return (T)_tokens.Where(x => x is T).FirstOrDefault();
		}
	}

    public class DebugInformation
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public DebugInformation(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}


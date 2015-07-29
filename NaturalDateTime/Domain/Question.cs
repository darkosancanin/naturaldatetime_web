using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NaturalDateTime.Domain;
using System.Diagnostics;

namespace NaturalDateTime
{
	public class Question
	{
		public string QuestionText { get; set; }
        public IList<DebugInformation> DebugInformation { get; set; }
        public string PreProcessedQuestionText { get; set; }
		private IList<Token> _tokens = new List<Token>();

        public Question(string question)
        {
            DebugInformation = new List<DebugInformation>();
            PreProcessedQuestionText = question;
            QuestionText = PreProcessText(question);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Tokenizers.GetAll().ForEach(x => x.TokenizeTheQuestion(this));
            AddDebugInformation("Tokenizing Question", String.Format("{0} ms, {1} ticks", stopwatch.ElapsedMilliseconds, stopwatch.ElapsedTicks));
            OrderTokens();
            
        }

        public void AddDebugInformation(string name, string value)
        {
            DebugInformation.Add(new DebugInformation(name, value));
        }

        public void ResolveTokenValues()
        {
            foreach (var token in _tokens)
                token.ResolveTokenValues();
        }

        public string PreProcessText(string text)
        {
			text = text.Trim().Trim('?').Trim();
            return Regex.Replace(text, @"\s+", " ", RegexOptions.Multiline);
        }
		
		public void AddToken(Token token){
            var overlappingTokens = _tokens.Where(x => (token.StartPosition >= x.StartPosition && token.StartPosition <= x.FinishPosition) || (token.FinishPosition >= x.StartPosition && token.FinishPosition <= x.FinishPosition)).ToList();
            if (overlappingTokens.Count > 0)
            {
                foreach (var overlappingToken in overlappingTokens)
                {
                    if(token.Priority <= overlappingToken.Priority)
                    {
                        _tokens.Remove(overlappingToken);
                        _tokens.Add(token);
                    }
                }
            }
            else
            {
                 _tokens.Add(token);
            }
        }
		
		public void AddTokens(IList<Token> tokens){
			foreach(var token in tokens){
				AddToken (token);
			}
		}
		
		public void OrderTokens(){
			_tokens = _tokens.OrderBy(x => x.StartPosition).ToList();	
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
            var orderedTokens = _tokens.OrderByDescending(x => x.StartPosition).ToList();
            foreach (Token token in orderedTokens)
            {
                formattedText = formattedText.Insert(token.FinishPosition, token.FormattedEndTag);
                formattedText = formattedText.Insert(token.StartPosition, token.FormattedStartTag);
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
				var matchingToken = _tokens.Skip(numberOfItemsToSkip).Where(x => tokenType.IsAssignableFrom(x.GetType())).FirstOrDefault();
				if(matchingToken == null) return false;
				numberOfItemsToSkip = _tokens.IndexOf(matchingToken) + 1;
			}
			return true;
        }
		
		public bool ContainsExactNumberOfMatches(Type tokenType, int numberOfOccurrences)
        {
            return _tokens.Where(x => tokenType.IsAssignableFrom(x.GetType())).Count() == numberOfOccurrences;
        }

        public bool Contains(params Type[] tokenTypes)
        {
            var tokens = _tokens.ToList();
            if (tokenTypes.Any(tokenType => !tokens.Exists(x => tokenType.IsAssignableFrom(x.GetType()))))
                return false;

            return true;
        }

		public T GetToken<T>(int whichOccuranceOfToken) where T : Token {
			return (T)_tokens.Where(x => x is T).Skip(whichOccuranceOfToken - 1).FirstOrDefault();
		}
		
		public T GetToken<T>() where T : Token {
			return (T)_tokens.Where(x => x is T).FirstOrDefault();
		}
	}
}


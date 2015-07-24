using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalDateTime
{
    public abstract class Token
    {
		public string Value { get; set; }

        public int StartPosition { get; set; }

        // Priority determines whether a token should be added if it overlaps with a existing token. E.g. the tokenization of the phrase 'eastern time' as a TimezoneToken should take precedence over the tokenization using the LiteralDateOrTimeToken of the word 'time' in that phrase.
        public abstract int Priority { get; }

        public Token(string value, int startPosition)
        {
			Value = value;
            StartPosition = startPosition;
            if(Value.StartsWith(" "))
            {
                Value = Value.TrimStart();
                StartPosition += 1;
            }
            Value = Value.Trim(',');
            Value = Value.TrimEnd();
        }
		
		public int LengthOfMatch 
		{ 
			get { return Value.Length; }
		}

        public int FinishPosition
        {
            get { return StartPosition + LengthOfMatch; }
        }

        public virtual string FormattedStartTag 
		{ 
			get { return String.Format("<{0}>", this.GetType().Name); }
		}

        public virtual void ResolveTokenValues()
        {
        }

        public virtual string FormattedEndTag 
		{ 
			get { return String.Format("</{0}>", this.GetType().Name);; }
		}
    }
}

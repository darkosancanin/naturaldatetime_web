using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalDateTime
{
    public abstract class Token
    {
		public string Value { get; set; }

        public int Position { get; set; }

        public Token(string value, int position)
        {
			Value = value;
            Position = position;
            if(Value.StartsWith(" "))
            {
                Value = Value.TrimStart();
                Position -= 1;
            }
            Value = Value.TrimEnd();
        }
		
		public int LengthOfMatch 
		{ 
			get { return Value.Length; }
		}
		
		public virtual string FormattedStartTag 
		{ 
			get { return String.Format("<{0}>", this.GetType().Name); }
		}
		
		public virtual string FormattedEndTag 
		{ 
			get { return String.Format("</{0}>", this.GetType().Name);; }
		}
    }
}

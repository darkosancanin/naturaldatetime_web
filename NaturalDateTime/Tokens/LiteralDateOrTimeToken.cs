using System;

namespace NaturalDateTime
{
	public class LiteralDateOrTimeToken : Token
	{
		public LiteralDateOrTimeToken (string value, int position) :base(value, position)
		{
		}

        public override int Priority
        {
            get { return 7; }
        }
    }
}


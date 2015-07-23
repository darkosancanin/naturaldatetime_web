using System;

namespace NaturalDateTime
{
	public class DaylightSavingsToken : Token
	{
		public DaylightSavingsToken (string value, int position) :base(value, position)
		{
		}

        public override int Priority
        {
            get { return 4; }
        }
    }
}


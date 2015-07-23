using System;
using NodaTime;

namespace NaturalDateTime
{
	public abstract class DateOrTimeToken : Token
	{
        public DateOrTimeToken(string value, int position) :base(value, position)
		{
        }
    }
}


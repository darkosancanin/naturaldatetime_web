using System;
using NodaTime;

namespace NaturalDateTime
{
	public abstract class CityOrTimezoneToken : Token
	{
        public CityOrTimezoneToken (string value, int position) :base(value, position)
		{
        }
        public abstract string GetFormattedNameAndTimezone(Instant instant);
        public abstract OffsetDateTime GetCurrentTimeAsOffsetDateTime();
        public abstract LocalDateTime GetLocalDateTime(Instant instant);
    }
}


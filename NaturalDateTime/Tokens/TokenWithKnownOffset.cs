using System;
using NodaTime;

namespace NaturalDateTime
{
	public abstract class TokenWithKnownOffset: Token
	{
        public TokenWithKnownOffset(string value, int position) :base(value, position)
		{
        }
        public abstract string GetFormattedNameAndTimezone(Instant instant);
        public abstract OffsetDateTime GetCurrentTimeAsOffsetDateTime();
        public abstract LocalDateTime GetLocalDateTime(Instant instant);
    }
}


using System;
using System.Linq;
using NodaTime;

namespace NaturalDateTime
{
	public class TimezoneToken : CityOrTimezoneToken 
    {
        public Timezone Timezone { get; set; }

        public override int Priority
        {
            get { return 1; }
        }

        public TimezoneToken(string value, int position) :base(value, position)
		{
		}

        public override OffsetDateTime GetCurrentTimeAsOffsetDateTime()
        {
            var instant = Instant.FromDateTimeUtc(DateTime.UtcNow);
            return instant.WithOffset(Timezone.Offset);
        }

        public override LocalDateTime GetLocalDateTime(Instant instant)
        {
            return instant.WithOffset(Timezone.Offset).LocalDateTime;
        }

        public override string GetFormattedNameAndTimezone(Instant instant)
        {
            return String.Format("{0} ({1})", Timezone.Name, Timezone.Abbreviation);
        }

        public override void ResolveTokenValues()
        {
            Timezone = Timezones.GetTimezoneByTokenValue(Value);
        }
    }
}


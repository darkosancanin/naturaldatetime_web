using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NodaTime;
using NodaTime.TimeZones;

namespace NaturalDateTime
{
    public class ConvertedZonedDateTimeResult
    {
        public UnambiguousZonedDateTimeResult UnambiguousKnownTimeZonedDateTimeResult { get; set; }
        public ZonedDateTime ConvertedZonedDateTime { get; set; }
		public string ConvertedZonedDateTimeTimezoneAbbreviation { get; set; }

        public ConvertedZonedDateTimeResult(UnambiguousZonedDateTimeResult unambiguousKnownTimeZonedDateTimeResult, ZonedDateTime convertedZonedDateTime)
        {
            UnambiguousKnownTimeZonedDateTimeResult = unambiguousKnownTimeZonedDateTimeResult;
            ConvertedZonedDateTime = convertedZonedDateTime;
			ConvertedZonedDateTimeTimezoneAbbreviation = convertedZonedDateTime.Zone.GetZoneInterval(convertedZonedDateTime.ToInstant()).Name;
        }
    }
}

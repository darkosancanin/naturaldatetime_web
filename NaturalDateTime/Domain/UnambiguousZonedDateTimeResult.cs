using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NodaTime;

namespace NaturalDateTime
{
    public class UnambiguousZonedDateTimeResult
    {
        public LocalDateTime OriginalLocalDateTime { get; set; }
        public ZonedDateTime UnambiguousZonedDateTime { get; set; }
		public string UnambiguousZonedDateTimeTimezoneAbbreviation { get; set; }
        public UnambiguousZonedDateTimeConversionResultType ConversionResultType { get; set; }

        public UnambiguousZonedDateTimeResult(LocalDateTime originalLocalDateTime, ZonedDateTime unambiguousZonedDateTime, UnambiguousZonedDateTimeConversionResultType conversionResultType)
        {
            OriginalLocalDateTime = originalLocalDateTime;
            UnambiguousZonedDateTime = unambiguousZonedDateTime;
            ConversionResultType = conversionResultType;
			UnambiguousZonedDateTimeTimezoneAbbreviation = unambiguousZonedDateTime.Zone.GetZoneInterval(unambiguousZonedDateTime.ToInstant()).Name;
		}
    }

    public enum UnambiguousZonedDateTimeConversionResultType
    {
        Unambiguous,
        Ambiguous,
        Skipped
    }
}

using System;
using NodaTime;
using NodaTime.TimeZones;

namespace NaturalDateTime
{
	public class DateTimeManager
	{
		public ConvertedZonedDateTimeResult GetCurrentTimeInTimezone(string timezoneId){
			var serverTimezone = DateTimeZoneProviders.Tzdb.GetSystemDefault() ?? DateTimeZoneProviders.Bcl.GetSystemDefault() ?? DateTimeZoneProviders.Tzdb["Australia/Sydney"]; 
            return ConvertDateTime(LocalDateTime.FromDateTime(DateTime.Now), serverTimezone, DateTimeZoneProviders.Tzdb[timezoneId]);
		}

        public DaylightSavingInfo GetCurrentDaylightSavingInfo(string timezoneId)
        {
            var dateTimeZone = DateTimeZoneProviders.Tzdb[timezoneId];
            var currentDateTime = GetCurrentTimeInTimezone(timezoneId).ConvertedZonedDateTime;
            UnambiguousZonedDateTimeResult unambiguousLocalDateTimeResult = GetUnambiguousZonedDateTime(currentDateTime.LocalDateTime, dateTimeZone);
            ZoneInterval zoneInterval = dateTimeZone.GetZoneInterval(unambiguousLocalDateTimeResult.UnambiguousZonedDateTime.ToInstant());
            if (zoneInterval.IsoLocalEnd.Year > 10000) return DaylightSavingInfo.CreateWithNoDaylightSavings();

            return GetDaylightSavingInfoFromZoneInterval(zoneInterval, dateTimeZone, currentDateTime);
        }

        public DaylightSavingInfo GetDaylightSavingInfo(int year, string timezoneId)
        {
            var dateTimeZone = DateTimeZoneProviders.Tzdb[timezoneId];
            UnambiguousZonedDateTimeResult endOfYearDateResult = GetUnambiguousZonedDateTime(new LocalDateTime(year, 1, 1, 0, 0), DateTimeZoneProviders.Tzdb[timezoneId]);
            ZoneInterval zoneInterval = dateTimeZone.GetZoneInterval(endOfYearDateResult.UnambiguousZonedDateTime.ToInstant());
            if (zoneInterval.IsoLocalEnd.Year > 10000) return DaylightSavingInfo.CreateWithNoDaylightSavings();

            var nextZoneIntervalThatStartsInThatYear = dateTimeZone.GetZoneInterval(GetUnambiguousZonedDateTime(zoneInterval.IsoLocalEnd.Plus(Period.FromMilliseconds(1)), dateTimeZone).UnambiguousZonedDateTime.ToInstant());

            return GetDaylightSavingInfoFromZoneInterval(nextZoneIntervalThatStartsInThatYear, dateTimeZone, GetCurrentTimeInTimezone(timezoneId).ConvertedZonedDateTime);
        }

        public DaylightSavingInfo GetDaylightSavingInfoFromZoneInterval(ZoneInterval zoneInterval, DateTimeZone dateTimeZone, ZonedDateTime currentDateTime)
        {
            var daylightSavingInfo = new DaylightSavingInfo { IsInDaylightSavingsTime = zoneInterval.Savings.Milliseconds > 0 };

            if (daylightSavingInfo.IsInDaylightSavingsTime)
            {
                daylightSavingInfo.End = zoneInterval.IsoLocalEnd;
                daylightSavingInfo.EndDateSavingPutBackInMilliseconds = zoneInterval.Savings.Milliseconds;
                daylightSavingInfo.HasEnded = daylightSavingInfo.End.ToDateTimeUnspecified() < currentDateTime.LocalDateTime.ToDateTimeUnspecified();
                daylightSavingInfo.Start = zoneInterval.IsoLocalStart.Minus(Period.FromMilliseconds(zoneInterval.Savings.Milliseconds));
                daylightSavingInfo.StartDateSavingPutForwardInMilliseconds = zoneInterval.Savings.Milliseconds;
                daylightSavingInfo.HasStarted = daylightSavingInfo.Start.ToDateTimeUnspecified() < currentDateTime.LocalDateTime.ToDateTimeUnspecified();
            }
            else
            {
                var previousZoneInterval = dateTimeZone.GetZoneInterval(GetUnambiguousZonedDateTime(zoneInterval.IsoLocalStart.Minus(Period.FromMilliseconds(1)), dateTimeZone).UnambiguousZonedDateTime.ToInstant());
                daylightSavingInfo.End = previousZoneInterval.IsoLocalEnd;
                daylightSavingInfo.EndDateSavingPutBackInMilliseconds = previousZoneInterval.Savings.Milliseconds;
                daylightSavingInfo.HasEnded = daylightSavingInfo.End.ToDateTimeUnspecified() < currentDateTime.LocalDateTime.ToDateTimeUnspecified();
                if (!daylightSavingInfo.NoDaylightSavings)
                {
                    var nextZoneInterval = dateTimeZone.GetZoneInterval(GetUnambiguousZonedDateTime(zoneInterval.IsoLocalEnd.Plus(Period.FromMilliseconds(1)), dateTimeZone).UnambiguousZonedDateTime.ToInstant());
                    daylightSavingInfo.Start = nextZoneInterval.IsoLocalStart.Minus(Period.FromMilliseconds(nextZoneInterval.Savings.Milliseconds));
                    daylightSavingInfo.StartDateSavingPutForwardInMilliseconds = nextZoneInterval.Savings.Milliseconds;
                    daylightSavingInfo.HasStarted = daylightSavingInfo.Start.ToDateTimeUnspecified() < currentDateTime.LocalDateTime.ToDateTimeUnspecified();
                }
            }

            return daylightSavingInfo;
        }

        public ConvertedZonedDateTimeResult ConvertDateTime(LocalDateTime convertFromDateTime, string convertFromTimezoneId, string convertToTimezoneId)
        {
            var convertFromTimezone = DateTimeZoneProviders.Tzdb[convertFromTimezoneId]; 
            var convertToTimezone = DateTimeZoneProviders.Tzdb[convertToTimezoneId];
            UnambiguousZonedDateTimeResult unambiguousLocalDateTimeResult = GetUnambiguousZonedDateTime(convertFromDateTime, convertFromTimezone);
            return new ConvertedZonedDateTimeResult(unambiguousLocalDateTimeResult, new ZonedDateTime(unambiguousLocalDateTimeResult.UnambiguousZonedDateTime.ToInstant(), convertToTimezone));
        }

        public ConvertedZonedDateTimeResult ConvertDateTime(LocalDateTime convertFromDateTime, DateTimeZone convertFromTimezone, DateTimeZone convertToTimezone)
        {
            UnambiguousZonedDateTimeResult unambiguousLocalDateTimeResult = GetUnambiguousZonedDateTime(convertFromDateTime, convertFromTimezone);
            return new ConvertedZonedDateTimeResult(unambiguousLocalDateTimeResult, new ZonedDateTime(unambiguousLocalDateTimeResult.UnambiguousZonedDateTime.ToInstant(), convertToTimezone));
        }

        public UnambiguousZonedDateTimeResult GetUnambiguousZonedDateTime(LocalDateTime localDateTime, DateTimeZone dateTimeZone)
        {
            ZoneLocalMapping convertedFromTimeMapping = dateTimeZone.MapLocal(localDateTime);
            UnambiguousZonedDateTimeConversionResultType conversionResultType;
            ZonedDateTime unambiguousZonedDateTime;
            if(convertedFromTimeMapping.Count == 1)
            {
                unambiguousZonedDateTime = convertedFromTimeMapping.Single();
                conversionResultType = UnambiguousZonedDateTimeConversionResultType.Unambiguous;
            }
            else if(convertedFromTimeMapping.Count > 1)
            {
                unambiguousZonedDateTime = convertedFromTimeMapping.First();
                conversionResultType = UnambiguousZonedDateTimeConversionResultType.Ambiguous;
            }
            else
            {
                unambiguousZonedDateTime = new ZonedDateTime(convertedFromTimeMapping.LateInterval.Start, dateTimeZone);
                conversionResultType = UnambiguousZonedDateTimeConversionResultType.Skipped;
            }

            return new UnambiguousZonedDateTimeResult(localDateTime, unambiguousZonedDateTime, conversionResultType);
        }

	    public LocalDateTimeResult GetLocalDateTime(DateToken knownDateToken, TimeToken timeToken, string timezone)
	    {
	        var currentDateTimeInTimezone = GetCurrentTimeInTimezone(timezone).ConvertedZonedDateTime;
	        var day = currentDateTimeInTimezone.Day;
            var month = currentDateTimeInTimezone.Month;
	        var year = currentDateTimeInTimezone.Year;
            if(knownDateToken != null)
            {
                if(knownDateToken.Day.HasValue) day = knownDateToken.Day.Value;
                if (knownDateToken.Month.HasValue) month = knownDateToken.Month.Value;
                if (knownDateToken.Year.HasValue) year = knownDateToken.Year.Value;
            }
	        var hour = 12;
	        var minute = 0;
            if(timeToken != null)
            {
                hour = timeToken.Hour;
				if (timeToken.Meridiem == Meridiem.AM && hour == 12) hour = 0;
                if (timeToken.Meridiem == Meridiem.PM && hour < 12) hour += 12;
                minute = timeToken.Minute ?? 0;
            }
			
			try{
            var localDateTime = new LocalDateTime(year, month, day, hour, minute);
            	return new LocalDateTimeResult(localDateTime);
			}
			catch(ArgumentOutOfRangeException){
				return new LocalDateTimeResult() { ResultType = LocalDateTimeResultType.FAILED };
			}
	    }
	}
}
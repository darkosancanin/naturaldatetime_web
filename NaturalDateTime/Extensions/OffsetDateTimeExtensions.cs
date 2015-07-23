using NaturalDateTime.Exceptions;
using NodaTime;
using NodaTime.TimeZones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalDateTime.Extensions
{
    public class OffsetDateTimeExtensions
    {
        public static OffsetDateTime CreateUpdatedOffsetDateTimeFromTokens(OffsetDateTime existingOffsetDateTime, DateToken dateToken, TimeToken timeToken)
        {
            var existingLocalDateTime = existingOffsetDateTime.LocalDateTime;
            var day = existingLocalDateTime.Day;
            var month = existingLocalDateTime.Month;
            var year = existingLocalDateTime.Year;
            if (dateToken != null)
            {
                if (dateToken.Day.HasValue) day = dateToken.Day.Value;
                if (dateToken.Month.HasValue) month = dateToken.Month.Value;
                if (dateToken.Year.HasValue) year = dateToken.Year.Value;
            }
            var hour = 12;
            var minute = 0;
            if (timeToken != null)
            {
                hour = timeToken.Hour;
                if (timeToken.Meridiem == Meridiem.AM && hour == 12) hour = 0;
                if (timeToken.Meridiem == Meridiem.PM && hour < 12) hour += 12;
                minute = timeToken.Minute ?? 0;
            }

            try
            {
                return new LocalDateTime(year, month, day, hour, minute).WithOffset(existingOffsetDateTime.Offset);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new InvalidTokenValueException(ErrorMessages.InvalidDateTime);
            }
        }
    }
}

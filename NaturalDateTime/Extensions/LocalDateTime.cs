using NaturalDateTime.Exceptions;
using NodaTime;
using System;
using System.Globalization;

namespace NaturalDateTime
{
	public static class LocalDateTimeExtensions
	{
		public static string GetFormattedTimeAndDate(this NodaTime.LocalDateTime localDateTime)
		{
		    var timeFormat = "h:mmtt";
            var minute = localDateTime.ToString("mm", CultureInfo.InvariantCulture);
            if (minute == "00") timeFormat = "htt";
            var formattedTime = localDateTime.ToString(timeFormat, CultureInfo.InvariantCulture).ToLower();
			formattedTime += " on ";
			formattedTime += localDateTime.ToString("dddd", CultureInfo.InvariantCulture);
			formattedTime += " the ";
			formattedTime += AddOrdinalToDay(int.Parse(localDateTime.ToString("dd", CultureInfo.InvariantCulture)));
			formattedTime += " of ";
			formattedTime += localDateTime.ToString("MMMM", CultureInfo.InvariantCulture);
            formattedTime += ", " + localDateTime.Year;
			return formattedTime;
		}

        public static string GetFormattedDateAndTime(this NodaTime.LocalDateTime localDateTime)
        {
            var timeFormat = "h:mmtt";
            var minute = localDateTime.ToString("mm", CultureInfo.InvariantCulture);
            if (minute == "00") timeFormat = "htt";
            var formattedTime = localDateTime.ToString("dddd", CultureInfo.InvariantCulture);
            formattedTime += " the ";
            formattedTime += AddOrdinalToDay(int.Parse(localDateTime.ToString("dd", CultureInfo.InvariantCulture)));
            formattedTime += " of ";
            formattedTime += localDateTime.ToString("MMMM", CultureInfo.InvariantCulture);
            formattedTime += ", " + localDateTime.Year;
            formattedTime += " at ";
            formattedTime += localDateTime.ToString(timeFormat, CultureInfo.InvariantCulture).ToLower();
            return formattedTime;
        }

        public static string AddOrdinalToDay(int day)
		{
			switch(day % 100)
	        {
	                case 11:
	                case 12:
	                case 13:
	                        return day.ToString() + "th";
	        }
	
	        switch(day % 10)
	        {
	                case 1:
	                        return day.ToString() + "st";
	                case 2:
	                        return day.ToString() + "nd";
	                case 3:
	                        return day.ToString() + "rd";
	                default:
	                        return day.ToString() + "th";
	        }
		}
	}
}


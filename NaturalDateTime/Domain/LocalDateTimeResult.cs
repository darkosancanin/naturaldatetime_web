using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NodaTime;
using NodaTime.TimeZones;

namespace NaturalDateTime
{
    public class LocalDateTimeResult
    {
        public LocalDateTime LocalDateTime { get; set; }
		public LocalDateTimeResultType ResultType { get; set; }

		public LocalDateTimeResult(){}
		
        public LocalDateTimeResult(LocalDateTime localDateTime)
        {
			LocalDateTime = localDateTime;
			ResultType = LocalDateTimeResultType.SUCCESSFUL;
        }
    }
	
	public enum LocalDateTimeResultType{
		SUCCESSFUL,
		FAILED
	}
}

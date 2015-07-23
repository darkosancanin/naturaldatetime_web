using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalDateTime
{
    public class DateToken : DateOrTimeToken
    {
		public int? Day { get;set; }
		public int? Month { get;set; }
		public int? Year { get;set; }
        public override int Priority
        {
            get { return 3; }
        }

        public DateToken (string value, int position, int? day, int? month, int? year):base(value, position)
        {
			Day = day;
			Month = month;
			Year = year;
        }
		
		public override string FormattedStartTag 
		{ 
			get 
			{
				var day = "";
				if(Day.HasValue) day = ":day=" + Day.Value;
				var month = "";
				if(Month.HasValue) month = ":month=" + Month.Value;
				var year = "";
				if(Year.HasValue) year = ":year=" + Year.Value;
				return String.Format("<{0}{1}{2}{3}>", GetType().Name, day, month, year);;
			}
		}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaturalDateTime
{
    public class TimeToken : DateOrTimeToken
    {
		public int Hour { get;set; }
		public int? Minute { get;set; }
		public Meridiem Meridiem { get;set; }

        public override int Priority
        {
            get { return 6; }
        }

        public TimeToken(string value, int position, int hour, int? minute, Meridiem meridiem):base(value, position)
        {
			Hour = hour;
			Minute = minute;
			Meridiem = meridiem;
        }
		
		public override string FormattedStartTag 
		{ 
			get 
			{
				var hour = ":hour=" + Hour;
				var minute = "";
				if(Minute.HasValue) minute = ":minute=" + Minute.Value;
				var meridiem = "";
				if(Meridiem != Meridiem.NONE) meridiem = ":meridiem=" + Meridiem.ToString().ToLower ();
                return String.Format("<{0}{1}{2}{3}>", GetType().Name, hour, minute, meridiem);
			}
		}
		
		public string FormattedTime{
			get 
			{
				var formattedTime = Hour.ToString();
				if(Meridiem == Meridiem.AM) formattedTime += "am";
				else if(Meridiem == Meridiem.PM) formattedTime += "pm";
				return formattedTime;
			}
		}
    }
}

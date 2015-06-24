using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NodaTime;

namespace NaturalDateTime
{
    public class DaylightSavingInfo
    {
        public LocalDateTime Start { get; set; }
        public LocalDateTime End { get; set; }
        public bool HasEnded { get; set; }
        public bool HasStarted { get; set; }
        public bool IsInDaylightSavingsTime { get; set; }
        public bool NoDaylightSavings { get; set; }
        public int StartDateSavingPutForwardInMilliseconds { get; set; }
        public int EndDateSavingPutBackInMilliseconds { get; set; }

        public static DaylightSavingInfo CreateWithNoDaylightSavings()
        {
            return new DaylightSavingInfo{NoDaylightSavings = true};
        }

        public bool IsCurrentDaylightSavingInfo
        {
            get { return (HasStarted && !HasEnded) || (!HasStarted && HasEnded); }
        }

        public string FormattedEndDateSavingPutBackInHours
        {
            get { return GetFormattedHoursFromMilliseconds(EndDateSavingPutBackInMilliseconds); }
        }

        public string FormattedStartDateSavingPutForwardInHours
        {
            get { return GetFormattedHoursFromMilliseconds(StartDateSavingPutForwardInMilliseconds); }
        }

        private string GetFormattedHoursFromMilliseconds(int milliseconds)
        {
            decimal totalHours = (decimal)milliseconds / 1000 / 60 / 60;
            var plural = totalHours > 1 ? "s" : "";
            return totalHours.ToString("0.##") + " hour" + plural;
        }
    }
}

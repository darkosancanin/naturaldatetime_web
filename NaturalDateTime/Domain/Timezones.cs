using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NaturalDateTime
{
    public static class Timezones
    {
        private static IList<Timezone> _timezones = new List<Timezone>()
        {
            new Timezone("Australian Central Daylight Time", "ACDT", Offset.FromHoursAndMinutes(10, 30)),
            new Timezone("Australian Central Standard Time", "ACDT", Offset.FromHoursAndMinutes(9, 30)),
            new Timezone("Australian Central Time", "ACT", "Australia/Adelaide"),
            new Timezone("Australian Central Western Standard Time", "ACWST", Offset.FromHoursAndMinutes(8, 45)),
            new Timezone("Atlantic Daylight Time", "ADT", Offset.FromHours(-3)),
            new Timezone("Atlantic Daylight Saving Time", "ADST", Offset.FromHours(-3)),
            new Timezone("Australian Eastern Daylight Time", "AEDT", Offset.FromHours(11)),
            new Timezone("Australian Eastern Standard Time", "AEST", Offset.FromHours(10)),
            new Timezone("Australian Eastern Time", "AET", "Australia/Sydney"),
            new Timezone("Alaska Daylight Time", "AKDT", Offset.FromHours(-8)),
            new Timezone("Alaska Standard Time", "AKST", Offset.FromHours(-9)),
            new Timezone("Brasilia Summer Time", "BRST", Offset.FromHours(-2)),
            new Timezone("Brasilia Time", "BRT", Offset.FromHours(-3)),
            new Timezone("Brazil Time", "BT", Offset.FromHours(-3)),
            new Timezone("British Daylight Time", "BDT", Offset.FromHours(1)),
            new Timezone("British Summer Time", "BST", Offset.FromHours(1)),
            new Timezone("Central Daylight Time", "CDT", Offset.FromHours(-5)),
            new Timezone("Central European Daylight Time", "CEDT", Offset.FromHours(2)),
            new Timezone("Central European Summer Time", "CEST", Offset.FromHours(2)),
            new Timezone("Central European Time", "CET", Offset.FromHours(1)),
            new Timezone("Central Standard Time", "CST", Offset.FromHours(-6)),
            new Timezone("Central Time", "CT", "US/Central"),
            new Timezone("Coordinated Universal Time", "UTC", Offset.FromHours(0)),
            new Timezone("Eastern Daylight Time", "EDT", Offset.FromHours(-4)),
            new Timezone("Eastern Standard Time", "EST", Offset.FromHours(-5)),
            new Timezone("Eastern Time", "ET", "US/Eastern"),
            new Timezone("Greenwich Mean Time", "GMT", Offset.FromHours(0)),
            new Timezone("Hawaii-Aleutian Daylight Time", "HADT", Offset.FromHours(-9)),
            new Timezone("Hawaii-Aleutian Standard Time", "HADT", Offset.FromHours(-10)),
            new Timezone("Hawaii Daylight Time", "HDT", Offset.FromHours(-9)),
            new Timezone("Hawaii Standard Time", "HST", Offset.FromHours(-10)),
            new Timezone("Hong Kong Time", "HKT", Offset.FromHours(8)),
            new Timezone("Israel Daylight Time", "IDT", Offset.FromHours(3)),
            new Timezone("Iran Standard Time", "IRST", Offset.FromHoursAndMinutes(3, 30)),
            new Timezone("India Standard Time", "IST", Offset.FromHoursAndMinutes(5, 30)),
            new Timezone("Japan Standard Time", "JST", Offset.FromHours(9)),
            new Timezone("Korea Standard Time", "KST", Offset.FromHours(9)),
            new Timezone("Mountain Daylight Time", "MDT", Offset.FromHours(-6)),
            new Timezone("Mountain Daylight Saving Time", "MDST", Offset.FromHours(-6)),
            new Timezone("Moscow Daylight Time", "MSD", Offset.FromHours(4)),
            new Timezone("Moscow Standard Time", "MSK", Offset.FromHours(3)),
            new Timezone("Mountain Standard Time", "MST", Offset.FromHours(-7)),
            new Timezone("Nepal Time", "NPT", Offset.FromHoursAndMinutes(5, 45)),
            new Timezone("New Zealand Daylight Time", "NZDT", Offset.FromHours(13)),
            new Timezone("New Zealand Standard Time", "NZST", Offset.FromHours(12)),
            new Timezone("Pacific Daylight Time", "PDT", Offset.FromHours(-7)),
            new Timezone("Pacific Standard Time", "PST", Offset.FromHours(-8)),
            new Timezone("Pacific Time", "PT", "US/Pacific"),
            new Timezone("Pakistan Standard Time", "PKT", Offset.FromHours(5)),
            new Timezone("Philippine Time", "PHT", Offset.FromHours(8))
        };

        public static IList<Timezone> GetAllTimezones()
        {
            return _timezones;
        }

        public static Timezone GetTimezoneByTokenValue(string value)
        {
            return GetAllTimezones().Where(x => x.Name.ToLower() == value.ToLower() || x.Abbreviation.ToLower() == value.ToLower()).First();
        }
    }

    public class Timezone
    {
        private Offset? _offset;
        public Offset Offset
        {
            get
            {
                if (_offset.HasValue)
                    return _offset.Value;
                else
                    return SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Tzdb[TimezoneId]).GetZoneInterval().WallOffset;
            }
        }
        public string Name { get; set; }
        public string TimezoneId { get; set; }

        public string Abbreviation { get; set; }

        public Timezone(string name, string abbreviation, Offset? offset)
        {
            Name = name;
            Abbreviation = abbreviation;
            _offset = offset;
            TimezoneId = null;
        }

        public Timezone(string name, string abbreviation, string timezoneId)
        {
            Name = name;
            Abbreviation = abbreviation;
            TimezoneId = timezoneId;
            _offset = null;
        }
    }
}


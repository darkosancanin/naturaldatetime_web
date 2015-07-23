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
            new Timezone("Australian Eastern Daylight Time", "AEDT", Offset.FromHours(11)),
            new Timezone("Australian Eastern Standard Time", "AEST", Offset.FromHours(10)),
            new Timezone("Australian Eastern Time", "AET", "Australia/Sydney"),
            new Timezone("British Daylight Time", "BDT", Offset.FromHours(1)),
            new Timezone("British Summer Time", "BST", Offset.FromHours(1)),
            new Timezone("Central Daylight Time", "CDT", Offset.FromHours(-5)),
            new Timezone("Central European Daylight Time", "CEDT", Offset.FromHours(2)),
            new Timezone("Central European Summer Time", "CEST", Offset.FromHours(2)),
            new Timezone("Central European Time", "CET", Offset.FromHours(1)),
            new Timezone("Central Standard Time", "CST", Offset.FromHours(-6)),
            new Timezone("Central Time", "CT", "US/Central"),
            new Timezone("Eastern Daylight Time", "EDT", Offset.FromHours(-4)),
            new Timezone("Eastern Standard Time", "EST", Offset.FromHours(-5)),
            new Timezone("Eastern Time", "ET", "US/Eastern"),
            new Timezone("Pacific Daylight Time", "PDT", Offset.FromHours(-7)),
            new Timezone("Pacific Standard Time", "PST", Offset.FromHours(-8)),
            new Timezone("Pacific Time", "ET", "US/Pacific"),
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


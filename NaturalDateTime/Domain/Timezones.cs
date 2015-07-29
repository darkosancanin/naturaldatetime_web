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
            new Timezone("Alma-Ata Time", "ALMT", Offset.FromHours(6)),
            new Timezone("Amazon Summer Time", "AMST", Offset.FromHours(-3)),
            new Timezone("Amazon Time", "AMT", Offset.FromHours(-4)),
            new Timezone("Anadyr Summer Time", "ANAST", Offset.FromHours(12)),
            new Timezone("Anadyr Time", "ANAT", Offset.FromHours(12)),
            new Timezone("Aqtobe Time", "AQTT", Offset.FromHours(5)),
            new Timezone("Argentina Time", "ART", Offset.FromHours(-3)),
            new Timezone("Azores Summer Time", "AZOST", Offset.FromHours(0)),
            new Timezone("Azores Time", "AZOT", Offset.FromHours(-1)),
            new Timezone("Azerbaijan Summer Time", "AZST", Offset.FromHours(5)),
            new Timezone("Azerbaijan Time", "AZT", Offset.FromHours(4)),
            new Timezone("Afghanistan Time", "AFT", Offset.FromHoursAndMinutes(4, 30)),
            new Timezone("Australian Central Daylight Time", "ACDT", Offset.FromHoursAndMinutes(10, 30)),
            new Timezone("Australian Central Standard Time", "ACST", Offset.FromHoursAndMinutes(9, 30)),
            new Timezone("Australian Central Time", "ACT", "Australia/Adelaide", false),
            new Timezone("Australian Central Western Standard Time", "ACWST", Offset.FromHoursAndMinutes(8, 45)),
            new Timezone("Atlantic Daylight Time", "ADT", Offset.FromHours(-3)),
            new Timezone("Atlantic Daylight Saving Time", "ADST", Offset.FromHours(-3)),
            new Timezone("Australian Eastern Daylight Time", "AEDT", Offset.FromHours(11)),
            new Timezone("Australian Eastern Standard Time", "AEST", Offset.FromHours(10)),
            new Timezone("Australian Eastern Time", "AET", "Australia/Sydney"),
            new Timezone("Australian Western Daylight Time", "AWDT", Offset.FromHours(9)),
            new Timezone("Australian Western Standard Time", "AWST", Offset.FromHours(8)),
            new Timezone("Alaska Daylight Time", "AKDT", Offset.FromHours(-8)),
            new Timezone("Alaska Standard Time", "AKST", Offset.FromHours(-9)),
            new Timezone("Bolivia Time", "BOT", Offset.FromHours(-4)),
            new Timezone("Brunei Darussalam Time", "BNT", Offset.FromHours(8)),
            new Timezone("Brasilia Summer Time", "BRST", Offset.FromHours(-2)),
            new Timezone("Brasilia Time", "BRT", Offset.FromHours(-3)),
            new Timezone("Brazil Time", "BT", Offset.FromHours(-3)),
            new Timezone("British Daylight Time", "BDT", Offset.FromHours(1)),
            new Timezone("British Summer Time", "BST", Offset.FromHours(1)),
            new Timezone("Bhutan Time", "BTT", Offset.FromHours(6)),
            new Timezone("Casey Time", "CAST", Offset.FromHours(8)),
            new Timezone("Central Africa Time", "CAT", Offset.FromHours(2)),
            new Timezone("Cocos Islands Time", "CCT", Offset.FromHoursAndMinutes(6, 30)),
            new Timezone("Chatham Island Daylight Time", "CHADT", Offset.FromHoursAndMinutes(13, 45)),
            new Timezone("Chatham Island Standard Time", "CHAST", Offset.FromHoursAndMinutes(12, 45)),
            new Timezone("Choibalsan Time", "CHOT", Offset.FromHours(8)),
            new Timezone("Chuuk Time", "CHUT", Offset.FromHours(10)),
            new Timezone("Cook Island Time", "CKT", Offset.FromHours(-10)),
            new Timezone("Chile Summer Time", "CLST", Offset.FromHours(-3)),
            new Timezone("Chile Standard Time", "CLT", Offset.FromHours(-3)),
            new Timezone("Colombia Time", "COT", Offset.FromHours(-5)),
            new Timezone("Cape Verde Time", "CVT", Offset.FromHours(-1)),
            new Timezone("Christmas Island Time", "CXT", Offset.FromHours(7)),
            new Timezone("Chamorro Standard Time", "ChST", Offset.FromHours(10)),
            new Timezone("Davis Time", "DAVT", Offset.FromHours(7)),
            new Timezone("Dumont-d'Urville Time", "DDUT", Offset.FromHours(10)),
            new Timezone("Easter Island Summer Time", "EASST", Offset.FromHours(-5)),
            new Timezone("Easter Island Standard Time", "EAST", Offset.FromHours(-5), false),
            new Timezone("Eastern Africa Time", "EAT", Offset.FromHours(3), false),
            new Timezone("Ecuador Time", "ECT", Offset.FromHours(-5)),
            new Timezone("Eastern Greenland Summer Time", "EGST", Offset.FromHours(0)),
            new Timezone("Eastern Greenland Time", "EGT", Offset.FromHours(-1)),
            new Timezone("Further-Eastern European Time", "FET", Offset.FromHours(3)),
            new Timezone("Falkland Island Time", "FKT", Offset.FromHours(-4)),
            new Timezone("Fernando de Noronha Time", "FNT", Offset.FromHours(-2)),
            new Timezone("Galapagos Time", "GALT", Offset.FromHours(-6)),
            new Timezone("Gambier Time", "GAMT", Offset.FromHours(-9)),
            new Timezone("Georgia Standard Time", "GET", Offset.FromHours(4)),
            new Timezone("French Guiana Time", "GFT", Offset.FromHours(-3)),
            new Timezone("Gilbert Island Time", "GILT", Offset.FromHours(12)),
            new Timezone("Gulf Standard Time", "GST", Offset.FromHours(4)),
            new Timezone("Guyana Time", "GYT", Offset.FromHours(-4)),
            new Timezone("Hovd Time", "HOVT", Offset.FromHours(7)),
            new Timezone("Indochina Time", "ICT", Offset.FromHours(7)),
            new Timezone("Irkutsk Summer Time", "IRKST", Offset.FromHours(9)),
            new Timezone("Irkutsk Time", "IRKT", Offset.FromHours(8)),
            new Timezone("Kyrgyzstan Time", "KGT", Offset.FromHours(6)),
            new Timezone("Kosrae Time", "KOST", Offset.FromHours(11)),
            new Timezone("Krasnoyarsk Summer Time", "KRAST", Offset.FromHours(8)),
            new Timezone("Krasnoyarsk Time", "KRAT", Offset.FromHours(7)),
            new Timezone("Kuybyshev Time", "KUYT", Offset.FromHours(4)),
            new Timezone("Lord Howe Daylight Time", "LHDT", Offset.FromHours(11)),
            new Timezone("Lord Howe Standard Time", "LHST", Offset.FromHoursAndMinutes(10, 30)),
            new Timezone("Line Islands Time", "LINT", Offset.FromHours(14)),
            new Timezone("Magadan Summer Time", "MAGST", Offset.FromHours(12)),
            new Timezone("Magadan Time", "MAGT", Offset.FromHours(10)),
            new Timezone("Marquesas Time", "MART", Offset.FromHoursAndMinutes(-9, 30)),
            new Timezone("Mawson Time", "MAWT", Offset.FromHours(5)),
            new Timezone("Marshall Islands Time", "MHT", Offset.FromHours(12)),
            new Timezone("Myanmar Time", "MMT", Offset.FromHoursAndMinutes(6, 30)),
            new Timezone("Mountain Time", "MT", "America/Denver", false),
            new Timezone("Mauritius Time", "MUT", Offset.FromHours(4)),
            new Timezone("Maldives Time", "MVT", Offset.FromHours(5)),
            new Timezone("Malaysia Time", "MYT", Offset.FromHours(8)),
            new Timezone("New Caledonia Time", "NCT", Offset.FromHours(11)),
            new Timezone("Norfolk Time", "NFT", Offset.FromHoursAndMinutes(11, 30)),
            new Timezone("Newfoundland Daylight Time", "NDT", Offset.FromHoursAndMinutes(-2, 30)),
            new Timezone("Novosibirsk Summer Time", "NOVST", Offset.FromHours(7)),
            new Timezone("Novosibirsk Time", "NOVT", Offset.FromHours(6)),
            new Timezone("Nauru Time", "NRT", Offset.FromHours(12)),
            new Timezone("Newfoundland Standard Time", "NST", Offset.FromHoursAndMinutes(-3, 30)),
            new Timezone("Niue Time", "NUT", Offset.FromHours(-11)),
            new Timezone("Omsk Summer Time", "OMSST", Offset.FromHours(7)),
            new Timezone("Omsk Standard Time", "OMST", Offset.FromHours(6)),
            new Timezone("Oral Time", "ORAT", Offset.FromHours(5)),
            new Timezone("Peru Time", "PET", Offset.FromHours(-5)),
            new Timezone("Kamchatka Summer Time", "PETST", Offset.FromHours(12)),
            new Timezone("Kamchatka Time", "PETT", Offset.FromHours(12)),
            new Timezone("Phoenix Island Time", "PHOT", Offset.FromHours(13)),
            new Timezone("Pierre & Miquelon Daylight Time", "PMDT", Offset.FromHours(-2)),
            new Timezone("Pierre & Miquelon Standard Time", "PMST", Offset.FromHours(-3)),
            new Timezone("Pohnpei Standard Time", "PONT", Offset.FromHours(11)),
            new Timezone("Palau Time", "PWT", Offset.FromHours(9)),
            new Timezone("Paraguay Summer Time", "PYST", Offset.FromHours(-3)),
            new Timezone("Paraguay Time", "PYT", Offset.FromHours(-4)),
            new Timezone("Qyzylorda Time", "QYZT", Offset.FromHours(6)),
            new Timezone("Reunion Time", "RET", Offset.FromHours(4)),
            new Timezone("Rothera Time", "ROTT", Offset.FromHours(-3)),
            new Timezone("Sakhalin Time", "SAKT", Offset.FromHours(10)),
            new Timezone("Samara Time", "SAMT", Offset.FromHours(4)),
            new Timezone("Seychelles Time", "SCT", Offset.FromHours(4)),
            new Timezone("Singapore Time", "SGT", Offset.FromHours(8)),
            new Timezone("Srednekolymsk Time", "SRET", Offset.FromHours(11)),
            new Timezone("Suriname Time", "SRT", Offset.FromHours(-3)),
            new Timezone("Syowa Time", "SYOT", Offset.FromHours(3)),
            new Timezone("French Southern and Antarctic Time", "TFT", Offset.FromHours(5)),
            new Timezone("Tajikistan Time", "TJT", Offset.FromHours(5)),
            new Timezone("Tokelau Time", "TKT", Offset.FromHours(13)),
            new Timezone("East Timor Time", "TLT", Offset.FromHours(9)),
            new Timezone("Turkmenistan Time", "TMT", Offset.FromHours(5)),
            new Timezone("Tonga Time", "TOT", Offset.FromHours(13)),
            new Timezone("Tuvalu Time", "TVT", Offset.FromHours(12)),
            new Timezone("Ulaanbaatar Time", "ULAT", Offset.FromHours(8)),
            new Timezone("Uruguay Summer Time", "UYST", Offset.FromHours(-2)),
            new Timezone("Uruguay Time", "UYT", Offset.FromHours(-3)),
            new Timezone("Uzbekistan Time", "UZT", Offset.FromHours(5)),
            new Timezone("Venezuelan Standard Time", "VET", Offset.FromHoursAndMinutes(-4, 30)),
            new Timezone("Vladivostok Summer Time", "VLAST", Offset.FromHours(11)),
            new Timezone("Vladivostok Time", "VLAT", Offset.FromHours(10)),
            new Timezone("Vostok Time", "VOST", Offset.FromHours(6)),
            new Timezone("Wake Time", "WAKT", Offset.FromHours(12)),
            new Timezone("Western Argentine Summer Time", "WARST", Offset.FromHours(-3)),
            new Timezone("West Africa Summer Time", "WAST", Offset.FromHours(2)),
            new Timezone("Wallis and Futuna Time", "WFT", Offset.FromHours(12)),
            new Timezone("Western Greenland Summer Time", "WGST", Offset.FromHours(-2)),
            new Timezone("West Greenland Time", "WGT", Offset.FromHours(-3)),
            new Timezone("Western Indonesian Time", "WIB", Offset.FromHours(7)),
            new Timezone("Eastern Indonesian Time", "WIT", Offset.FromHours(9)),
            new Timezone("Central Indonesian Time", "WITA", Offset.FromHours(8)),
            new Timezone("West Samoa Time", "WST", Offset.FromHours(13)),
            new Timezone("Western Sahara Standard Time", "WT", Offset.FromHours(0)),
            new Timezone("Yakutsk Summer Time", "YAKST", Offset.FromHours(10)),
            new Timezone("Yakutsk Time", "YAKT", Offset.FromHours(9)),
            new Timezone("Yap Time", "YAPT", Offset.FromHours(10)),
            new Timezone("Yekaterinburg Summer Time", "YEKST", Offset.FromHours(6)),
            new Timezone("Yekaterinburg Time", "YEKT", Offset.FromHours(5)),
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
            new Timezone("Eastern European Summer Time", "EEST", Offset.FromHours(3)),
            new Timezone("Eastern European Time", "EET", Offset.FromHours(2)),
            new Timezone("Fiji Summer Time", "FJST", Offset.FromHours(13)),
            new Timezone("Fiji Time", "FJT", Offset.FromHours(12)),
            new Timezone("Greenwich Mean Time", "GMT", Offset.FromHours(0)),
            new Timezone("Hawaii-Aleutian Daylight Time", "HADT", Offset.FromHours(-9)),
            new Timezone("Hawaii-Aleutian Standard Time", "HAST", Offset.FromHours(-10)),
            new Timezone("Hawaii Daylight Time", "HDT", Offset.FromHours(-9)),
            new Timezone("Hawaii Standard Time", "HST", Offset.FromHours(-10)),
            new Timezone("Hong Kong Time", "HKT", Offset.FromHours(8)),
            new Timezone("Israel Daylight Time", "IDT", Offset.FromHours(3)),
            new Timezone("Iran Daylight Time", "IRDT", Offset.FromHoursAndMinutes(4, 30)),
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
            new Timezone("Papua New Guinea Time", "PGT", Offset.FromHours(10)),
            new Timezone("Philippine Time", "PHT", Offset.FromHours(8)),
            new Timezone("Samoa Standard Time", "SST", Offset.FromHours(-11)),
            new Timezone("South Africa Standard Time", "SAST", Offset.FromHours(2)),
            new Timezone("Solomon Islands Time", "SBT", Offset.FromHours(11)),
            new Timezone("Tahiti Time", "TAHT", Offset.FromHours(-10)),
            new Timezone("Vanuatu Time", "VUT", Offset.FromHours(11)),
            new Timezone("Western European Time", "WET", Offset.FromHours(0)),
            new Timezone("Western European Summer Time", "WEST", Offset.FromHours(1), false),
            new Timezone("Western European Daylight Time", "WEDT", Offset.FromHours(1))

        };

        public static IList<Timezone> GetAllTimezones()
        {
            return _timezones;
        }

        public static Timezone GetTimezoneByTokenValue(string value)
        {
            return GetAllTimezones().Where(x => x.Name.ToLower() == value.ToLower() || x.Abbreviation.ToLower() == value.ToLower()).Single();
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
        public bool TokenizeOnAbbreviation { get; set; }

        public Timezone(string name, string abbreviation, Offset? offset, bool tokenizeOnAbbreviation = true)
        {
            Name = name;
            Abbreviation = abbreviation;
            _offset = offset;
            TimezoneId = null;
            TokenizeOnAbbreviation = tokenizeOnAbbreviation;
        }

        public Timezone(string name, string abbreviation, string timezoneId, bool tokenizeOnAbbreviation = true)
        {
            Name = name;
            Abbreviation = abbreviation;
            TimezoneId = timezoneId;
            _offset = null;
            TokenizeOnAbbreviation = tokenizeOnAbbreviation;
        }
    }
}


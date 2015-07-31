﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalDateTime
{
    public static class CountryCodes
    {
        private static readonly Dictionary<string, string> _countryCodeLookup = new Dictionary<string, string>();
        private static readonly Dictionary<string, string> _twoLetterCountryCodeConversion = new Dictionary<string, string>();

        static CountryCodes()
        {
            InitializeCountryCodes();
            InitializeTwoLetterCountryCodeConversions();
        }

        public static string LookupCountryCode(string countryName)
        {
            if (countryName.Length == 2)
            {
                if (_twoLetterCountryCodeConversion.ContainsKey(countryName.ToLower()))
                    return _twoLetterCountryCodeConversion[countryName.ToLower()];
                return countryName;
            }

            if (_countryCodeLookup.ContainsKey(countryName.ToLower()))
                return _countryCodeLookup[countryName.ToLower()];

            return null;
        }

        private static void InitializeTwoLetterCountryCodeConversions()
        {
            _twoLetterCountryCodeConversion.Add("uk", "GB");
            _twoLetterCountryCodeConversion.Add("cg", "cd");
        }

        private static void InitializeCountryCodes()
        {
            _countryCodeLookup.Add("united states of america", "US");
            _countryCodeLookup.Add("u.s.a", "US");
            _countryCodeLookup.Add("bosnia", "BA");
            _countryCodeLookup.Add("bosna", "BA");
            _countryCodeLookup.Add("bosnia-herzegovina", "BA");
            _countryCodeLookup.Add("great britain", "GB");
            _countryCodeLookup.Add("britain", "GB");
            _countryCodeLookup.Add("england", "GB");
            _countryCodeLookup.Add("u.k", "GB");
            _countryCodeLookup.Add("andorra", "AD");
            _countryCodeLookup.Add("united arab emirates", "AE");
            _countryCodeLookup.Add("afghanistan", "AF");
            _countryCodeLookup.Add("antigua and barbuda", "AG");
            _countryCodeLookup.Add("anguilla", "AI");
            _countryCodeLookup.Add("albania", "AL");
            _countryCodeLookup.Add("armenia", "AM");
            _countryCodeLookup.Add("angola", "AO");
            _countryCodeLookup.Add("antarctica", "AQ");
            _countryCodeLookup.Add("argentina", "AR");
            _countryCodeLookup.Add("american samoa", "AS");
            _countryCodeLookup.Add("austria", "AT");
            _countryCodeLookup.Add("australia", "AU");
            _countryCodeLookup.Add("aruba", "AW");
            _countryCodeLookup.Add("åland islands", "AX");
            _countryCodeLookup.Add("azerbaijan", "AZ");
            _countryCodeLookup.Add("bosnia and herzegovina", "BA");
            _countryCodeLookup.Add("barbados", "BB");
            _countryCodeLookup.Add("bangladesh", "BD");
            _countryCodeLookup.Add("belgium", "BE");
            _countryCodeLookup.Add("burkina faso", "BF");
            _countryCodeLookup.Add("bulgaria", "BG");
            _countryCodeLookup.Add("bahrain", "BH");
            _countryCodeLookup.Add("burundi", "BI");
            _countryCodeLookup.Add("benin", "BJ");
            _countryCodeLookup.Add("saint barthélemy", "BL");
            _countryCodeLookup.Add("bermuda", "BM");
            _countryCodeLookup.Add("brunei darussalam", "BN");
            _countryCodeLookup.Add("bolivia", "BO");
            _countryCodeLookup.Add("sint eustatius", "BQ");
            _countryCodeLookup.Add("brazil", "BR");
            _countryCodeLookup.Add("bahamas", "BS");
            _countryCodeLookup.Add("bhutan", "BT");
            _countryCodeLookup.Add("bouvet island", "BV");
            _countryCodeLookup.Add("botswana", "BW");
            _countryCodeLookup.Add("belarus", "BY");
            _countryCodeLookup.Add("belize", "BZ");
            _countryCodeLookup.Add("canada", "CA");
            _countryCodeLookup.Add("cocos (keeling) islands", "CC");
            _countryCodeLookup.Add("congo", "CD");
            _countryCodeLookup.Add("central african republic", "CF");
            _countryCodeLookup.Add("switzerland", "CH");
            _countryCodeLookup.Add("côte d'ivoire", "CI");
            _countryCodeLookup.Add("cook islands", "CK");
            _countryCodeLookup.Add("chile", "CL");
            _countryCodeLookup.Add("cameroon", "CM");
            _countryCodeLookup.Add("china", "CN");
            _countryCodeLookup.Add("colombia", "CO");
            _countryCodeLookup.Add("costa rica", "CR");
            _countryCodeLookup.Add("cuba", "CU");
            _countryCodeLookup.Add("cape verde", "CV");
            _countryCodeLookup.Add("curaçao", "CW");
            _countryCodeLookup.Add("christmas island", "CX");
            _countryCodeLookup.Add("cyprus", "CY");
            _countryCodeLookup.Add("czech republic", "CZ");
            _countryCodeLookup.Add("germany", "DE");
            _countryCodeLookup.Add("djibouti", "DJ");
            _countryCodeLookup.Add("denmark", "DK");
            _countryCodeLookup.Add("dominica", "DM");
            _countryCodeLookup.Add("dominican republic", "DO");
            _countryCodeLookup.Add("algeria", "DZ");
            _countryCodeLookup.Add("ecuador", "EC");
            _countryCodeLookup.Add("estonia", "EE");
            _countryCodeLookup.Add("egypt", "EG");
            _countryCodeLookup.Add("western sahara", "EH");
            _countryCodeLookup.Add("eritrea", "ER");
            _countryCodeLookup.Add("spain", "ES");
            _countryCodeLookup.Add("ethiopia", "ET");
            _countryCodeLookup.Add("finland", "FI");
            _countryCodeLookup.Add("fiji", "FJ");
            _countryCodeLookup.Add("falkland islands (malvinas)", "FK");
            _countryCodeLookup.Add("micronesia", "FM");
            _countryCodeLookup.Add("faroe islands", "FO");
            _countryCodeLookup.Add("france", "FR");
            _countryCodeLookup.Add("gabon", "GA");
            _countryCodeLookup.Add("united kingdom", "GB");
            _countryCodeLookup.Add("grenada", "GD");
            _countryCodeLookup.Add("georgia", "GE");
            _countryCodeLookup.Add("french guiana", "GF");
            _countryCodeLookup.Add("guernsey", "GG");
            _countryCodeLookup.Add("ghana", "GH");
            _countryCodeLookup.Add("gibraltar", "GI");
            _countryCodeLookup.Add("greenland", "GL");
            _countryCodeLookup.Add("gambia", "GM");
            _countryCodeLookup.Add("guinea", "GN");
            _countryCodeLookup.Add("guadeloupe", "GP");
            _countryCodeLookup.Add("equatorial guinea", "GQ");
            _countryCodeLookup.Add("greece", "GR");
            _countryCodeLookup.Add("south georgia and the south sandwich islands", "GS");
            _countryCodeLookup.Add("guatemala", "GT");
            _countryCodeLookup.Add("guam", "GU");
            _countryCodeLookup.Add("guinea-bissau", "GW");
            _countryCodeLookup.Add("guyana", "GY");
            _countryCodeLookup.Add("hong kong", "HK");
            _countryCodeLookup.Add("heard island and mcdonald islands", "HM");
            _countryCodeLookup.Add("honduras", "HN");
            _countryCodeLookup.Add("croatia", "HR");
            _countryCodeLookup.Add("haiti", "HT");
            _countryCodeLookup.Add("hungary", "HU");
            _countryCodeLookup.Add("indonesia", "ID");
            _countryCodeLookup.Add("ireland", "IE");
            _countryCodeLookup.Add("israel", "IL");
            _countryCodeLookup.Add("isle of man", "IM");
            _countryCodeLookup.Add("india", "IN");
            _countryCodeLookup.Add("british indian ocean territory", "IO");
            _countryCodeLookup.Add("iraq", "IQ");
            _countryCodeLookup.Add("iran", "IR");
            _countryCodeLookup.Add("iceland", "IS");
            _countryCodeLookup.Add("italy", "IT");
            _countryCodeLookup.Add("jersey", "JE");
            _countryCodeLookup.Add("jamaica", "JM");
            _countryCodeLookup.Add("jordan", "JO");
            _countryCodeLookup.Add("japan", "JP");
            _countryCodeLookup.Add("kenya", "KE");
            _countryCodeLookup.Add("kyrgyzstan", "KG");
            _countryCodeLookup.Add("cambodia", "KH");
            _countryCodeLookup.Add("kiribati", "KI");
            _countryCodeLookup.Add("comoros", "KM");
            _countryCodeLookup.Add("saint kitts and nevis", "KN");
            _countryCodeLookup.Add("democratic people's republic of korea", "KP");
            _countryCodeLookup.Add("republic of korea", "KR");
            _countryCodeLookup.Add("kuwait", "KW");
            _countryCodeLookup.Add("cayman islands", "KY");
            _countryCodeLookup.Add("kazakhstan", "KZ");
            _countryCodeLookup.Add("lao people's democratic republic", "LA");
            _countryCodeLookup.Add("lebanon", "LB");
            _countryCodeLookup.Add("saint lucia", "LC");
            _countryCodeLookup.Add("liechtenstein", "LI");
            _countryCodeLookup.Add("sri lanka", "LK");
            _countryCodeLookup.Add("liberia", "LR");
            _countryCodeLookup.Add("lesotho", "LS");
            _countryCodeLookup.Add("lithuania", "LT");
            _countryCodeLookup.Add("luxembourg", "LU");
            _countryCodeLookup.Add("latvia", "LV");
            _countryCodeLookup.Add("libyan arab jamahiriya", "LY");
            _countryCodeLookup.Add("morocco", "MA");
            _countryCodeLookup.Add("monaco", "MC");
            _countryCodeLookup.Add("republic of moldova", "MD");
            _countryCodeLookup.Add("montenegro", "ME");
            _countryCodeLookup.Add("saint martin (french part)", "MF");
            _countryCodeLookup.Add("madagascar", "MG");
            _countryCodeLookup.Add("marshall islands", "MH");
            _countryCodeLookup.Add("macedonia", "MK");
            _countryCodeLookup.Add("mali", "ML");
            _countryCodeLookup.Add("myanmar", "MM");
            _countryCodeLookup.Add("mongolia", "MN");
            _countryCodeLookup.Add("macao", "MO");
            _countryCodeLookup.Add("northern mariana islands", "MP");
            _countryCodeLookup.Add("martinique", "MQ");
            _countryCodeLookup.Add("mauritania", "MR");
            _countryCodeLookup.Add("montserrat", "MS");
            _countryCodeLookup.Add("malta", "MT");
            _countryCodeLookup.Add("mauritius", "MU");
            _countryCodeLookup.Add("maldives", "MV");
            _countryCodeLookup.Add("malawi", "MW");
            _countryCodeLookup.Add("mexico", "MX");
            _countryCodeLookup.Add("malaysia", "MY");
            _countryCodeLookup.Add("mozambique", "MZ");
            _countryCodeLookup.Add("namibia", "NA");
            _countryCodeLookup.Add("new caledonia", "NC");
            _countryCodeLookup.Add("niger", "NE");
            _countryCodeLookup.Add("norfolk island", "NF");
            _countryCodeLookup.Add("nigeria", "NG");
            _countryCodeLookup.Add("nicaragua", "NI");
            _countryCodeLookup.Add("netherlands", "NL");
            _countryCodeLookup.Add("norway", "NO");
            _countryCodeLookup.Add("nepal", "NP");
            _countryCodeLookup.Add("nauru", "NR");
            _countryCodeLookup.Add("niue", "NU");
            _countryCodeLookup.Add("new zealand", "NZ");
            _countryCodeLookup.Add("oman", "OM");
            _countryCodeLookup.Add("panama", "PA");
            _countryCodeLookup.Add("peru", "PE");
            _countryCodeLookup.Add("french polynesia", "PF");
            _countryCodeLookup.Add("papua new guinea", "PG");
            _countryCodeLookup.Add("philippines", "PH");
            _countryCodeLookup.Add("pakistan", "PK");
            _countryCodeLookup.Add("poland", "PL");
            _countryCodeLookup.Add("saint pierre and miquelon", "PM");
            _countryCodeLookup.Add("pitcairn", "PN");
            _countryCodeLookup.Add("puerto rico", "PR");
            _countryCodeLookup.Add("occupied palestinian territory", "PS");
            _countryCodeLookup.Add("portugal", "PT");
            _countryCodeLookup.Add("palau", "PW");
            _countryCodeLookup.Add("paraguay", "PY");
            _countryCodeLookup.Add("qatar", "QA");
            _countryCodeLookup.Add("réunion", "RE");
            _countryCodeLookup.Add("romania", "RO");
            _countryCodeLookup.Add("serbia", "RS");
            _countryCodeLookup.Add("russian federation", "RU");
            _countryCodeLookup.Add("rwanda", "RW");
            _countryCodeLookup.Add("saudi arabia", "SA");
            _countryCodeLookup.Add("solomon islands", "SB");
            _countryCodeLookup.Add("seychelles", "SC");
            _countryCodeLookup.Add("sudan", "SD");
            _countryCodeLookup.Add("sweden", "SE");
            _countryCodeLookup.Add("singapore", "SG");
            _countryCodeLookup.Add("saint helena, ascension and tristan da cunha", "SH");
            _countryCodeLookup.Add("slovenia", "SI");
            _countryCodeLookup.Add("svalbard and jan mayen", "SJ");
            _countryCodeLookup.Add("slovakia", "SK");
            _countryCodeLookup.Add("sierra leone", "SL");
            _countryCodeLookup.Add("san marino", "SM");
            _countryCodeLookup.Add("senegal", "SN");
            _countryCodeLookup.Add("somalia", "SO");
            _countryCodeLookup.Add("suriname", "SR");
            _countryCodeLookup.Add("south sudan", "SS");
            _countryCodeLookup.Add("sao tome and principe", "ST");
            _countryCodeLookup.Add("el salvador", "SV");
            _countryCodeLookup.Add("sint maarten", "SX");
            _countryCodeLookup.Add("syrian arab republic", "SY");
            _countryCodeLookup.Add("swaziland", "SZ");
            _countryCodeLookup.Add("turks and caicos islands", "TC");
            _countryCodeLookup.Add("chad", "TD");
            _countryCodeLookup.Add("french southern territories", "TF");
            _countryCodeLookup.Add("togo", "TG");
            _countryCodeLookup.Add("thailand", "TH");
            _countryCodeLookup.Add("tajikistan", "TJ");
            _countryCodeLookup.Add("tokelau", "TK");
            _countryCodeLookup.Add("timor-leste", "TL");
            _countryCodeLookup.Add("turkmenistan", "TM");
            _countryCodeLookup.Add("tunisia", "TN");
            _countryCodeLookup.Add("tonga", "TO");
            _countryCodeLookup.Add("turkey", "TR");
            _countryCodeLookup.Add("trinidad and tobago", "TT");
            _countryCodeLookup.Add("tuvalu", "TV");
            _countryCodeLookup.Add("taiwan", "TW");
            _countryCodeLookup.Add("tanzania", "TZ");
            _countryCodeLookup.Add("ukraine", "UA");
            _countryCodeLookup.Add("uganda", "UG");
            _countryCodeLookup.Add("united states minor outlying islands", "UM");
            _countryCodeLookup.Add("united states", "US");
            _countryCodeLookup.Add("uruguay", "UY");
            _countryCodeLookup.Add("uzbekistan", "UZ");
            _countryCodeLookup.Add("holy see (vatican city state)", "VA");
            _countryCodeLookup.Add("saint vincent and the grenadines", "VC");
            _countryCodeLookup.Add("venezuela", "VE");
            _countryCodeLookup.Add("british virgin islands", "VG");
            _countryCodeLookup.Add("u.s. virgin islands", "VI");
            _countryCodeLookup.Add("viet nam", "VN");
            _countryCodeLookup.Add("vanuatu", "VU");
            _countryCodeLookup.Add("wallis and futuna", "WF");
            _countryCodeLookup.Add("samoa", "WS");
            _countryCodeLookup.Add("kosovo", "XK");
            _countryCodeLookup.Add("yemen", "YE");
            _countryCodeLookup.Add("mayotte", "YT");
            _countryCodeLookup.Add("south africa", "ZA");
            _countryCodeLookup.Add("zambia", "ZM");
            _countryCodeLookup.Add("zimbabwe", "ZW");
            _countryCodeLookup.Add("afg", "AF");
            _countryCodeLookup.Add("ala", "AX");
            _countryCodeLookup.Add("alb", "AL");
            _countryCodeLookup.Add("dza", "DZ");
            _countryCodeLookup.Add("asm", "AS");
            _countryCodeLookup.Add("and", "AD");
            _countryCodeLookup.Add("ago", "AO");
            _countryCodeLookup.Add("aia", "AI");
            _countryCodeLookup.Add("atg", "AG");
            _countryCodeLookup.Add("arg", "AR");
            _countryCodeLookup.Add("arm", "AM");
            _countryCodeLookup.Add("abw", "AW");
            _countryCodeLookup.Add("aus", "AU");
            _countryCodeLookup.Add("aut", "AT");
            _countryCodeLookup.Add("aze", "AZ");
            _countryCodeLookup.Add("bhs", "BS");
            _countryCodeLookup.Add("bhr", "BH");
            _countryCodeLookup.Add("bgd", "BD");
            _countryCodeLookup.Add("brb", "BB");
            _countryCodeLookup.Add("blr", "BY");
            _countryCodeLookup.Add("bel", "BE");
            _countryCodeLookup.Add("blz", "BZ");
            _countryCodeLookup.Add("ben", "BJ");
            _countryCodeLookup.Add("bmu", "BM");
            _countryCodeLookup.Add("btn", "BT");
            _countryCodeLookup.Add("bol", "BO");
            _countryCodeLookup.Add("bes", "BQ");
            _countryCodeLookup.Add("bih", "BA");
            _countryCodeLookup.Add("bwa", "BW");
            _countryCodeLookup.Add("bra", "BR");
            _countryCodeLookup.Add("iot", "IO");
            _countryCodeLookup.Add("vgb", "VG");
            _countryCodeLookup.Add("brn", "BN");
            _countryCodeLookup.Add("bgr", "BG");
            _countryCodeLookup.Add("bfa", "BF");
            _countryCodeLookup.Add("bdi", "BI");
            _countryCodeLookup.Add("khm", "KH");
            _countryCodeLookup.Add("cmr", "CM");
            _countryCodeLookup.Add("can", "CA");
            _countryCodeLookup.Add("cpv", "CV");
            _countryCodeLookup.Add("cym", "KY");
            _countryCodeLookup.Add("caf", "CF");
            _countryCodeLookup.Add("tcd", "TD");
            _countryCodeLookup.Add("chl", "CL");
            _countryCodeLookup.Add("chn", "CN");
            _countryCodeLookup.Add("cxr", "CX");
            _countryCodeLookup.Add("cck", "CC");
            _countryCodeLookup.Add("col", "CO");
            _countryCodeLookup.Add("com", "KM");
            _countryCodeLookup.Add("cog", "CG");
            _countryCodeLookup.Add("zar", "CD");
            _countryCodeLookup.Add("cok", "CK");
            _countryCodeLookup.Add("cri", "CR");
            _countryCodeLookup.Add("hrv", "HR");
            _countryCodeLookup.Add("cub", "CU");
            _countryCodeLookup.Add("cuw", "CW");
            _countryCodeLookup.Add("cyp", "CY");
            _countryCodeLookup.Add("cze", "CZ");
            _countryCodeLookup.Add("dnk", "DK");
            _countryCodeLookup.Add("dji", "DJ");
            _countryCodeLookup.Add("dma", "DM");
            _countryCodeLookup.Add("dom", "DO");
            _countryCodeLookup.Add("tls", "TL");
            _countryCodeLookup.Add("ecu", "EC");
            _countryCodeLookup.Add("egy", "EG");
            _countryCodeLookup.Add("slv", "SV");
            _countryCodeLookup.Add("gnq", "GQ");
            _countryCodeLookup.Add("eri", "ER");
            _countryCodeLookup.Add("est", "EE");
            _countryCodeLookup.Add("eth", "ET");
            _countryCodeLookup.Add("fro", "FO");
            _countryCodeLookup.Add("flk", "FK");
            _countryCodeLookup.Add("fji", "FJ");
            _countryCodeLookup.Add("fin", "FI");
            _countryCodeLookup.Add("fra", "FR");
            _countryCodeLookup.Add("guf", "GF");
            _countryCodeLookup.Add("pyf", "PF");
            _countryCodeLookup.Add("atf", "TF");
            _countryCodeLookup.Add("gab", "GA");
            _countryCodeLookup.Add("gmb", "GM");
            _countryCodeLookup.Add("geo", "GE");
            _countryCodeLookup.Add("deu", "DE");
            _countryCodeLookup.Add("gha", "GH");
            _countryCodeLookup.Add("gib", "GI");
            _countryCodeLookup.Add("grc", "GR");
            _countryCodeLookup.Add("grl", "GL");
            _countryCodeLookup.Add("grd", "GD");
            _countryCodeLookup.Add("glp", "GP");
            _countryCodeLookup.Add("gum", "GU");
            _countryCodeLookup.Add("gtm", "GT");
            _countryCodeLookup.Add("ggy", "GG");
            _countryCodeLookup.Add("gin", "GN");
            _countryCodeLookup.Add("gnb", "GW");
            _countryCodeLookup.Add("guy", "GY");
            _countryCodeLookup.Add("hti", "HT");
            _countryCodeLookup.Add("vat", "VA");
            _countryCodeLookup.Add("hnd", "HN");
            _countryCodeLookup.Add("hkg", "HK");
            _countryCodeLookup.Add("hun", "HU");
            _countryCodeLookup.Add("isl", "IS");
            _countryCodeLookup.Add("ind", "IN");
            _countryCodeLookup.Add("idn", "ID");
            _countryCodeLookup.Add("irn", "IR");
            _countryCodeLookup.Add("irq", "IQ");
            _countryCodeLookup.Add("irl", "IE");
            _countryCodeLookup.Add("imn", "IM");
            _countryCodeLookup.Add("isr", "IL");
            _countryCodeLookup.Add("ita", "IT");
            _countryCodeLookup.Add("civ", "CI");
            _countryCodeLookup.Add("jam", "JM");
            _countryCodeLookup.Add("jpn", "JP");
            _countryCodeLookup.Add("jey", "JE");
            _countryCodeLookup.Add("jor", "JO");
            _countryCodeLookup.Add("kaz", "KZ");
            _countryCodeLookup.Add("ken", "KE");
            _countryCodeLookup.Add("kir", "KI");
            _countryCodeLookup.Add("kwt", "KW");
            _countryCodeLookup.Add("kgz", "KG");
            _countryCodeLookup.Add("lao", "LA");
            _countryCodeLookup.Add("lva", "LV");
            _countryCodeLookup.Add("lbn", "LB");
            _countryCodeLookup.Add("lso", "LS");
            _countryCodeLookup.Add("lbr", "LR");
            _countryCodeLookup.Add("lby", "LY");
            _countryCodeLookup.Add("lie", "LI");
            _countryCodeLookup.Add("ltu", "LT");
            _countryCodeLookup.Add("lux", "LU");
            _countryCodeLookup.Add("mac", "MO");
            _countryCodeLookup.Add("mkd", "MK");
            _countryCodeLookup.Add("mdg", "MG");
            _countryCodeLookup.Add("mwi", "MW");
            _countryCodeLookup.Add("mys", "MY");
            _countryCodeLookup.Add("mdv", "MV");
            _countryCodeLookup.Add("mli", "ML");
            _countryCodeLookup.Add("mlt", "MT");
            _countryCodeLookup.Add("mhl", "MH");
            _countryCodeLookup.Add("mtq", "MQ");
            _countryCodeLookup.Add("mrt", "MR");
            _countryCodeLookup.Add("mus", "MU");
            _countryCodeLookup.Add("myt", "YT");
            _countryCodeLookup.Add("mex", "MX");
            _countryCodeLookup.Add("fsm", "FM");
            _countryCodeLookup.Add("mda", "MD");
            _countryCodeLookup.Add("mco", "MC");
            _countryCodeLookup.Add("mng", "MN");
            _countryCodeLookup.Add("mne", "ME");
            _countryCodeLookup.Add("msr", "MS");
            _countryCodeLookup.Add("mar", "MA");
            _countryCodeLookup.Add("moz", "MZ");
            _countryCodeLookup.Add("mmr", "MM");
            _countryCodeLookup.Add("nam", "NA");
            _countryCodeLookup.Add("nru", "NR");
            _countryCodeLookup.Add("npl", "NP");
            _countryCodeLookup.Add("ant", "AN");
            _countryCodeLookup.Add("nld", "NL");
            _countryCodeLookup.Add("ncl", "NC");
            _countryCodeLookup.Add("nzl", "NZ");
            _countryCodeLookup.Add("nic", "NI");
            _countryCodeLookup.Add("ner", "NE");
            _countryCodeLookup.Add("nga", "NG");
            _countryCodeLookup.Add("niu", "NU");
            _countryCodeLookup.Add("nfk", "NF");
            _countryCodeLookup.Add("prk", "KP");
            _countryCodeLookup.Add("mnp", "MP");
            _countryCodeLookup.Add("nor", "NO");
            _countryCodeLookup.Add("omn", "OM");
            _countryCodeLookup.Add("pak", "PK");
            _countryCodeLookup.Add("plw", "PW");
            _countryCodeLookup.Add("pse", "PS");
            _countryCodeLookup.Add("pan", "PA");
            _countryCodeLookup.Add("png", "PG");
            _countryCodeLookup.Add("pry", "PY");
            _countryCodeLookup.Add("per", "PE");
            _countryCodeLookup.Add("phl", "PH");
            _countryCodeLookup.Add("pcn", "PN");
            _countryCodeLookup.Add("pol", "PL");
            _countryCodeLookup.Add("prt", "PT");
            _countryCodeLookup.Add("pri", "PR");
            _countryCodeLookup.Add("qat", "QA");
            _countryCodeLookup.Add("rou", "RO");
            _countryCodeLookup.Add("rus", "RU");
            _countryCodeLookup.Add("rwa", "RW");
            _countryCodeLookup.Add("reu", "RE");
            _countryCodeLookup.Add("blm", "BL");
            _countryCodeLookup.Add("kna", "KN");
            _countryCodeLookup.Add("shn", "SH");
            _countryCodeLookup.Add("lca", "LC");
            _countryCodeLookup.Add("maf", "MF");
            _countryCodeLookup.Add("spm", "PM");
            _countryCodeLookup.Add("vct", "VC");
            _countryCodeLookup.Add("wsm", "WS");
            _countryCodeLookup.Add("smr", "SM");
            _countryCodeLookup.Add("stp", "ST");
            _countryCodeLookup.Add("sau", "SA");
            _countryCodeLookup.Add("sen", "SN");
            _countryCodeLookup.Add("srb", "RS");
            _countryCodeLookup.Add("syc", "SC");
            _countryCodeLookup.Add("sle", "SL");
            _countryCodeLookup.Add("sgp", "SG");
            _countryCodeLookup.Add("sxm", "SX");
            _countryCodeLookup.Add("svk", "SK");
            _countryCodeLookup.Add("svn", "SI");
            _countryCodeLookup.Add("slb", "SB");
            _countryCodeLookup.Add("som", "SO");
            _countryCodeLookup.Add("zaf", "ZA");
            _countryCodeLookup.Add("sgs", "GS");
            _countryCodeLookup.Add("kor", "KR");
            _countryCodeLookup.Add("ssd", "SS");
            _countryCodeLookup.Add("esp", "ES");
            _countryCodeLookup.Add("lka", "LK");
            _countryCodeLookup.Add("sdn", "SD");
            _countryCodeLookup.Add("sur", "SR");
            _countryCodeLookup.Add("swz", "SZ");
            _countryCodeLookup.Add("swe", "SE");
            _countryCodeLookup.Add("che", "CH");
            _countryCodeLookup.Add("syr", "SY");
            _countryCodeLookup.Add("twn", "TW");
            _countryCodeLookup.Add("tjk", "TJ");
            _countryCodeLookup.Add("tza", "TZ");
            _countryCodeLookup.Add("tha", "TH");
            _countryCodeLookup.Add("tgo", "TG");
            _countryCodeLookup.Add("tkl", "TK");
            _countryCodeLookup.Add("ton", "TO");
            _countryCodeLookup.Add("tto", "TT");
            _countryCodeLookup.Add("tun", "TN");
            _countryCodeLookup.Add("tur", "TR");
            _countryCodeLookup.Add("tkm", "TM");
            _countryCodeLookup.Add("tca", "TC");
            _countryCodeLookup.Add("tuv", "TV");
            _countryCodeLookup.Add("uga", "UG");
            _countryCodeLookup.Add("ukr", "UA");
            _countryCodeLookup.Add("are", "AE");
            _countryCodeLookup.Add("gbr", "GB");
            _countryCodeLookup.Add("usa", "US");
            _countryCodeLookup.Add("vir", "VI");
            _countryCodeLookup.Add("ury", "UY");
            _countryCodeLookup.Add("uzb", "UZ");
            _countryCodeLookup.Add("vut", "VU");
            _countryCodeLookup.Add("ven", "VE");
            _countryCodeLookup.Add("vnm", "VN");
            _countryCodeLookup.Add("wlf", "WF");
            _countryCodeLookup.Add("esh", "EH");
            _countryCodeLookup.Add("yem", "YE");
            _countryCodeLookup.Add("zmb", "ZM");
            _countryCodeLookup.Add("zwe", "ZW");
        }
    }
}

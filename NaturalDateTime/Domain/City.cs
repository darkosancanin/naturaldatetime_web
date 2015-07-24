using System;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Search.Spans;
using Lucene.Net.Store;

namespace NaturalDateTime
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AsciiName { get; set; }
        public string AlternateNames { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
		public string AdministrativeDivisionName { get; set; }
		public string AdministrativeDivisionNameAcronym { get; set; }
		public string AdministrativeDivisionAsciiName { get; set; }
        public string Timezone { get; set; }
        public long Population { get; set; }

        public City (Document document){
			Id = int.Parse(document.Get(CityFieldNames.Id));
			Name = document.Get(CityFieldNames.Name);
			AsciiName = document.Get(CityFieldNames.AsciiName);
			AlternateNames = document.Get(CityFieldNames.AlternateNames);
			Latitude = decimal.Parse(document.Get(CityFieldNames.Latitude));
			Longitude = decimal.Parse(document.Get(CityFieldNames.Longitude));
			CountryCode = document.Get(CityFieldNames.CountryCode);
			CountryName = document.Get(CityFieldNames.CountryName);
			AdministrativeDivisionName = document.Get(CityFieldNames.AdministrativeDivisionName);
			AdministrativeDivisionNameAcronym = document.Get(CityFieldNames.AdministrativeDivisionNameAcronym);
			AdministrativeDivisionAsciiName = document.Get(CityFieldNames.AdministrativeDivisionAsciiName);
			Timezone = document.Get(CityFieldNames.Timezone);
			Population = long.Parse(document.Get(CityFieldNames.Population));
		}

        public string FormattedName
        {
            get 
			{ 
				var adminDiv = string.Empty;
				if(!string.IsNullOrEmpty(AdministrativeDivisionName) 
				   && AdministrativeDivisionName != CountryName
				   && AdministrativeDivisionName != Name) 
					adminDiv = ", " + AdministrativeDivisionName;
				
				var countryName = string.Empty;
				if(CountryName != Name) 
					countryName = ", " + CountryName;
				
				return Name + adminDiv + countryName; 
			}
        }

        public string GetSitemapCityName()
        {
            var cityName = AsciiName ?? Name;
            cityName = cityName.Replace('’', '\'');
            var adminDiv = string.Empty;
            if (!string.IsNullOrEmpty(AdministrativeDivisionName)
               && AdministrativeDivisionName != CountryName
               && AdministrativeDivisionName != Name
               && !cityName.Contains(AdministrativeDivisionName))
                adminDiv = ", " + AdministrativeDivisionName;

            var countryName = string.Empty;
            if (CountryName != Name && !cityName.Contains(CountryName))
                countryName = ", " + CountryName;

            cityName += adminDiv + countryName;
            cityName = cityName.Replace(" ", "_").Replace(".", "");
            return cityName;
        }

        public string GetFormattedNameAndTimezone(string timezoneAbbreviation)
        {
			return FormattedName + " (" + timezoneAbbreviation + ")"; 
        }
		
		public bool HasNoTimezone{
			get { return String.IsNullOrEmpty(Timezone); }	
		}

        public static City FindCity(CityToken cityToken)
        {
            var analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            var queryParser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, CityFieldNames.Name, analyzer);
            var searcher = new IndexSearcher(FSDirectory.Open(ApplicationSettings.CityIndexDirectory), true);
            var sort = new Sort(new[] { new SortField(CityFieldNames.Population, SortField.LONG, true), SortField.FIELD_SCORE });

            var possibleCityDetails = cityToken.GetPotentialCityDetails();
            foreach (var possibleCityDetail in possibleCityDetails)
            {
                var topScoreDocCollector = TopFieldCollector.Create(sort, 1, true, false, false, false);
                var countryCode = string.Empty;
                if (!string.IsNullOrEmpty(possibleCityDetail.CountryName))
                {
                    countryCode = CountryCodes.LookupCountryCode(possibleCityDetail.CountryName);
                    if (string.IsNullOrEmpty(countryCode)) continue;
                }
                var queryText = GetQueryText(possibleCityDetail.CityName, countryCode, possibleCityDetail.AdministrativeDivisionName);

                var query = queryParser.Parse(queryText);
                searcher.Search(query, topScoreDocCollector);
                var results = topScoreDocCollector.TopDocs().ScoreDocs;

                if (topScoreDocCollector.TotalHits > 0)
                {
                    var docId = results[0].Doc;
                    var document = searcher.Doc(docId);
                    var city = new City(document);
                    return city;
                }
            }

            return null;
        }

        private static string GetQueryText(string cityName, string countryCode, string administrativeDivision)
        {
            var queryText = String.Format("({1}:\"{0}\" OR {2}:\"{0}\" OR {3}:\"{0}\")", QueryParser.Escape(cityName), CityFieldNames.Name, CityFieldNames.AlternateNames, CityFieldNames.AsciiName);

            if (!string.IsNullOrEmpty(administrativeDivision))
                queryText += String.Format(" AND ({1}:\"{0}\" OR {2}:\"{0}\" OR {3}:\"{0}\")", QueryParser.Escape(administrativeDivision), CityFieldNames.AdministrativeDivisionName, CityFieldNames.AdministrativeDivisionAsciiName, CityFieldNames.AdministrativeDivisionNameAcronym);

            if (!string.IsNullOrEmpty(countryCode))
                queryText += String.Format(" AND {0}:\"{1}\"", CityFieldNames.CountryCode, countryCode);

            return queryText;
        }
    }

    public class CityFieldNames
    {
        public static string Id = "id";
        public static string Name = "name";
        public static string AsciiName = "ascii_name";
        public static string AlternateNames = "alternate_names";
        public static string Latitude = "latitude";
        public static string Longitude = "longitude";
        public static string CountryCode = "country_code";
        public static string CountryName = "country_name";
		public static string AdministrativeDivisionName = "administrative_division_name";
		public static string AdministrativeDivisionNameAcronym = "administrative_division_name_acronym";
		public static string AdministrativeDivisionAsciiName = "administrative_division_ascii_name";
        public static string Timezone = "timezone";
        public static string Population = "population";
    }
}


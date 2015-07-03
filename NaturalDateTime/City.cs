using System;
using Lucene.Net.Documents;

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
            cityName = cityName.Replace(".", "");
            return cityName;
        }

        public string GetFormattedNameAndTimezone(string timezoneAbbreviation)
        {
			return FormattedName + " (" + timezoneAbbreviation + ")"; 
        }
		
		public bool HasNoTimezone{
			get { return String.IsNullOrEmpty(Timezone); }	
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


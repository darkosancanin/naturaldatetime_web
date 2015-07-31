using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using NaturalDateTime.Exceptions;
using NodaTime;

namespace NaturalDateTime
{
    public class CityToken : CityOrTimezoneToken 
    {
        public City City { get; set; }

        public override int Priority
        {
            get { return 5; }
        }
        public CityToken (string value, int position) :base(value, position)
        {
        }

        public override OffsetDateTime GetCurrentTimeAsOffsetDateTime()
        {
            return SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Tzdb[City.Timezone]).ToOffsetDateTime();
        }

        public ZonedDateTime GetCurrentTime()
        {
            return SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Tzdb[City.Timezone]);
        }

        public override LocalDateTime GetLocalDateTime(Instant instant)
        {
            return instant.InZone(DateTimeZoneProviders.Tzdb[City.Timezone]).LocalDateTime;
        }

        public override string GetFormattedNameAndTimezone(Instant instant)
        {
            var timezoneAbbreviation = instant.InZone(DateTimeZoneProviders.Tzdb[City.Timezone]).GetZoneInterval().Name;
            return City.GetFormattedNameAndTimezone(timezoneAbbreviation);
        }

        public override void ResolveTokenValues()
        {
            City = City.FindCity(this);

            if (City == null)
                throw new InvalidTokenValueException(ErrorMessages.UnableToRecognizeCity);

            if (City.HasNoTimezone)
                throw new InvalidTokenValueException(ErrorMessages.NoTimezone);
        }

        public IList<CityTokenPossibleCityDetails> GetPossibleCityDetails()
        {
            var cityDetails = new List<CityTokenPossibleCityDetails>();
			Value = Value.Replace(".", " ");
			var positionOfFirstComma = Value.IndexOf(',');
            if(positionOfFirstComma > 0)
            {
				String firstValue = Value.Substring(0, positionOfFirstComma).Trim();
				String secondValue = null;
				String thirdValue = null;
				var startPositionOfSecondValue = positionOfFirstComma + 1;

				var positionOfSecondComma = Value.IndexOf(',', positionOfFirstComma + 1);
				if(positionOfSecondComma > 0){
					secondValue = Value.Substring(startPositionOfSecondValue, positionOfSecondComma - startPositionOfSecondValue).Trim();
					var startPositionOfThirdValue = positionOfSecondComma + 1;
					thirdValue = Value.Substring(startPositionOfThirdValue).Trim();
				}
				else{
					secondValue = Value.Substring(startPositionOfSecondValue).Trim();
				}

				if(!String.IsNullOrEmpty(thirdValue)){
					cityDetails.Add(new CityTokenPossibleCityDetails(firstValue, thirdValue) { AdministrativeDivisionName = secondValue });
				}
				else if(!String.IsNullOrEmpty(secondValue)){
					cityDetails.Add(new CityTokenPossibleCityDetails(firstValue, secondValue));
					cityDetails.Add(new CityTokenPossibleCityDetails(firstValue) { AdministrativeDivisionName = secondValue });
				}
				else{
					cityDetails.Add(new CityTokenPossibleCityDetails(firstValue));
				}
            }
            else
            {
                cityDetails.Add(new CityTokenPossibleCityDetails(Value));
                var firstOccurenceOfSpace = Value.IndexOf(' ');
                if(firstOccurenceOfSpace > 0)
                {
                    var cityName = Value.Substring(0, firstOccurenceOfSpace).Trim();
                    var countryOrAdministrativeDivision = Value.Substring(cityName.Length + 1).Trim();
                    cityDetails.Add(new CityTokenPossibleCityDetails(cityName, countryOrAdministrativeDivision));
					cityDetails.Add(new CityTokenPossibleCityDetails(cityName) { AdministrativeDivisionName = countryOrAdministrativeDivision });
                }

                var secondOccurenceOfSpace = Value.IndexOf(' ', firstOccurenceOfSpace + 1);
                if (secondOccurenceOfSpace > 0)
                {
                    var cityName = Value.Substring(0, secondOccurenceOfSpace).Trim();
                    var countryOrAdministrativeDivision = Value.Substring(cityName.Length + 1).Trim();
                    cityDetails.Add(new CityTokenPossibleCityDetails(cityName, countryOrAdministrativeDivision));
					cityDetails.Add(new CityTokenPossibleCityDetails(cityName) { AdministrativeDivisionName = countryOrAdministrativeDivision });
                }
            }

            return cityDetails;
        } 
    }

    public class CityTokenPossibleCityDetails
    {
        public string CityName { get; set; }
        public string CountryName { get; set; }
		public string AdministrativeDivisionName { get; set; }

        public CityTokenPossibleCityDetails(string cityName)
        {
            CityName = cityName.Trim();
        }
		
		public CityTokenPossibleCityDetails(string cityName, string countryName)
        {
            CityName = cityName;
            CountryName = countryName;
        }
    }
}

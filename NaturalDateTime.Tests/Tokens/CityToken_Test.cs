using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NaturalDateTime.Tests
{
    [TestFixture]
    public class CityToken_Test
    {
        [Test]
        public void GetPossibleCityDetails_should_return_the_best_three_guesses_at_a_match()
        {
            var cityToken = new CityToken("Sydney, Australia", 0);
            var possibleCityDetails = cityToken.GetPossibleCityDetails();
            Assert.AreEqual(2, cityToken.GetPossibleCityDetails().Count);
            var firstPossibleCityDetails = possibleCityDetails.First();
            Assert.AreEqual("Sydney", firstPossibleCityDetails.CityName);
            Assert.AreEqual("Australia", firstPossibleCityDetails.CountryName);
			var secondPossibleCityDetails = possibleCityDetails[1];
            Assert.AreEqual("Sydney", secondPossibleCityDetails.CityName);
            Assert.AreEqual("Australia", secondPossibleCityDetails.AdministrativeDivisionName);

			
            cityToken = new CityToken("Sydney Australia", 0);
            possibleCityDetails = cityToken.GetPossibleCityDetails();
            Assert.AreEqual(3, possibleCityDetails.Count);
            firstPossibleCityDetails = possibleCityDetails.First();
            Assert.AreEqual("Sydney Australia", firstPossibleCityDetails.CityName);
            Assert.IsNull(firstPossibleCityDetails.CountryName);
            secondPossibleCityDetails = possibleCityDetails[1];
            Assert.AreEqual("Sydney", secondPossibleCityDetails.CityName);
            Assert.AreEqual("Australia", secondPossibleCityDetails.CountryName);
			var thirdPossibleCityDetails = possibleCityDetails[2];
            Assert.AreEqual("Sydney", thirdPossibleCityDetails.CityName);
            Assert.AreEqual("Australia", thirdPossibleCityDetails.AdministrativeDivisionName);


            cityToken = new CityToken("Auckland, New Zealand", 0);
            possibleCityDetails = cityToken.GetPossibleCityDetails();
            Assert.AreEqual(2, possibleCityDetails.Count);
            possibleCityDetails = cityToken.GetPossibleCityDetails();
            firstPossibleCityDetails = possibleCityDetails.First();
            Assert.AreEqual("Auckland", firstPossibleCityDetails.CityName);
            Assert.AreEqual("New Zealand", firstPossibleCityDetails.CountryName);
			secondPossibleCityDetails = possibleCityDetails[1];
            Assert.AreEqual("Auckland", secondPossibleCityDetails.CityName);
            Assert.AreEqual("New Zealand", secondPossibleCityDetails.AdministrativeDivisionName);


            cityToken = new CityToken("Auckland New Zealand", 0);
            possibleCityDetails = cityToken.GetPossibleCityDetails();
            Assert.AreEqual(5, possibleCityDetails.Count);
            firstPossibleCityDetails = possibleCityDetails.First();
            Assert.AreEqual("Auckland New Zealand", firstPossibleCityDetails.CityName);
            Assert.IsNull(firstPossibleCityDetails.CountryName);
            secondPossibleCityDetails = possibleCityDetails[1];
            Assert.AreEqual("Auckland", secondPossibleCityDetails.CityName);
            Assert.AreEqual("New Zealand", secondPossibleCityDetails.CountryName);
            thirdPossibleCityDetails = possibleCityDetails[2];
            Assert.AreEqual("Auckland", thirdPossibleCityDetails.CityName);
            Assert.AreEqual("New Zealand", thirdPossibleCityDetails.AdministrativeDivisionName);
			var fourthPossibleCityDetails = possibleCityDetails[3];
            Assert.AreEqual("Auckland New", fourthPossibleCityDetails.CityName);
            Assert.AreEqual("Zealand", fourthPossibleCityDetails.CountryName);
			var fifthPossibleCityDetails = possibleCityDetails[4];
            Assert.AreEqual("Auckland New", fifthPossibleCityDetails.CityName);
            Assert.AreEqual("Zealand", fifthPossibleCityDetails.AdministrativeDivisionName);
			
			
			cityToken = new CityToken("St.Pierre", 0);
            possibleCityDetails = cityToken.GetPossibleCityDetails();
            Assert.AreEqual(3, possibleCityDetails.Count);
            possibleCityDetails = cityToken.GetPossibleCityDetails();
            firstPossibleCityDetails = possibleCityDetails.First();
            Assert.AreEqual("St Pierre", firstPossibleCityDetails.CityName);
            Assert.IsNullOrEmpty( firstPossibleCityDetails.CountryName);
			secondPossibleCityDetails = possibleCityDetails[1];
            Assert.AreEqual("St", secondPossibleCityDetails.CityName);
            Assert.AreEqual("Pierre", secondPossibleCityDetails.CountryName);
			thirdPossibleCityDetails = possibleCityDetails[2];
            Assert.AreEqual("St", thirdPossibleCityDetails.CityName);
            Assert.AreEqual("Pierre", thirdPossibleCityDetails.AdministrativeDivisionName);

			cityToken = new CityToken("Sydney, New South Wales, Australia", 0);
            possibleCityDetails = cityToken.GetPossibleCityDetails();
            Assert.AreEqual(1, cityToken.GetPossibleCityDetails().Count);
            firstPossibleCityDetails = possibleCityDetails.First();
            Assert.AreEqual("Sydney", firstPossibleCityDetails.CityName);
			Assert.AreEqual("New South Wales", firstPossibleCityDetails.AdministrativeDivisionName);
            Assert.AreEqual("Australia", firstPossibleCityDetails.CountryName);
        }

		[Test]
        public void GetpossibleCityDetails_should_handle_bad_positioned_commas()
        {
            var cityToken = new CityToken("Sydney,", 0);
            var possibleCityDetails = cityToken.GetPossibleCityDetails();
            Assert.AreEqual(1, cityToken.GetPossibleCityDetails().Count);
            var firstPossibleCityDetails = possibleCityDetails.First();
            Assert.AreEqual("Sydney", firstPossibleCityDetails.CityName);
            Assert.IsNull(firstPossibleCityDetails.CountryName);

			cityToken = new CityToken("Sydney, Australia,", 0);
            possibleCityDetails = cityToken.GetPossibleCityDetails();
            Assert.AreEqual(2, cityToken.GetPossibleCityDetails().Count);
            firstPossibleCityDetails = possibleCityDetails.First();
            Assert.AreEqual("Sydney", firstPossibleCityDetails.CityName);
            Assert.AreEqual("Australia", firstPossibleCityDetails.CountryName);
			var secondPossibleCityDetails = possibleCityDetails[1];
            Assert.AreEqual("Sydney", secondPossibleCityDetails.CityName);
            Assert.AreEqual("Australia", secondPossibleCityDetails.AdministrativeDivisionName);
		}
    }
}

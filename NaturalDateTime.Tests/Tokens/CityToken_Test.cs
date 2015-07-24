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
        public void GetPotentialCityDetails_should_return_the_best_three_guesses_at_a_match()
        {
            var cityToken = new CityToken("Sydney, Australia", 0);
            var potentialCityDetails = cityToken.GetPotentialCityDetails();
            Assert.AreEqual(2, cityToken.GetPotentialCityDetails().Count);
            var firstPossibleCityDetails = potentialCityDetails.First();
            Assert.AreEqual("Sydney", firstPossibleCityDetails.CityName);
            Assert.AreEqual("Australia", firstPossibleCityDetails.CountryName);
			var secondPossibleCityDetails = potentialCityDetails[1];
            Assert.AreEqual("Sydney", secondPossibleCityDetails.CityName);
            Assert.AreEqual("Australia", secondPossibleCityDetails.AdministrativeDivisionName);

			
            cityToken = new CityToken("Sydney Australia", 0);
            potentialCityDetails = cityToken.GetPotentialCityDetails();
            Assert.AreEqual(3, potentialCityDetails.Count);
            firstPossibleCityDetails = potentialCityDetails.First();
            Assert.AreEqual("Sydney Australia", firstPossibleCityDetails.CityName);
            Assert.IsNull(firstPossibleCityDetails.CountryName);
            secondPossibleCityDetails = potentialCityDetails[1];
            Assert.AreEqual("Sydney", secondPossibleCityDetails.CityName);
            Assert.AreEqual("Australia", secondPossibleCityDetails.CountryName);
			var thirdPossibleCityDetails = potentialCityDetails[2];
            Assert.AreEqual("Sydney", thirdPossibleCityDetails.CityName);
            Assert.AreEqual("Australia", thirdPossibleCityDetails.AdministrativeDivisionName);


            cityToken = new CityToken("Auckland, New Zealand", 0);
            potentialCityDetails = cityToken.GetPotentialCityDetails();
            Assert.AreEqual(2, potentialCityDetails.Count);
            potentialCityDetails = cityToken.GetPotentialCityDetails();
            firstPossibleCityDetails = potentialCityDetails.First();
            Assert.AreEqual("Auckland", firstPossibleCityDetails.CityName);
            Assert.AreEqual("New Zealand", firstPossibleCityDetails.CountryName);
			secondPossibleCityDetails = potentialCityDetails[1];
            Assert.AreEqual("Auckland", secondPossibleCityDetails.CityName);
            Assert.AreEqual("New Zealand", secondPossibleCityDetails.AdministrativeDivisionName);


            cityToken = new CityToken("Auckland New Zealand", 0);
            potentialCityDetails = cityToken.GetPotentialCityDetails();
            Assert.AreEqual(5, potentialCityDetails.Count);
            firstPossibleCityDetails = potentialCityDetails.First();
            Assert.AreEqual("Auckland New Zealand", firstPossibleCityDetails.CityName);
            Assert.IsNull(firstPossibleCityDetails.CountryName);
            secondPossibleCityDetails = potentialCityDetails[1];
            Assert.AreEqual("Auckland", secondPossibleCityDetails.CityName);
            Assert.AreEqual("New Zealand", secondPossibleCityDetails.CountryName);
            thirdPossibleCityDetails = potentialCityDetails[2];
            Assert.AreEqual("Auckland", thirdPossibleCityDetails.CityName);
            Assert.AreEqual("New Zealand", thirdPossibleCityDetails.AdministrativeDivisionName);
			var fourthPossibleCityDetails = potentialCityDetails[3];
            Assert.AreEqual("Auckland New", fourthPossibleCityDetails.CityName);
            Assert.AreEqual("Zealand", fourthPossibleCityDetails.CountryName);
			var fifthPossibleCityDetails = potentialCityDetails[4];
            Assert.AreEqual("Auckland New", fifthPossibleCityDetails.CityName);
            Assert.AreEqual("Zealand", fifthPossibleCityDetails.AdministrativeDivisionName);
			
			
			cityToken = new CityToken("St.Pierre", 0);
            potentialCityDetails = cityToken.GetPotentialCityDetails();
            Assert.AreEqual(3, potentialCityDetails.Count);
            potentialCityDetails = cityToken.GetPotentialCityDetails();
            firstPossibleCityDetails = potentialCityDetails.First();
            Assert.AreEqual("St Pierre", firstPossibleCityDetails.CityName);
            Assert.IsNullOrEmpty( firstPossibleCityDetails.CountryName);
			secondPossibleCityDetails = potentialCityDetails[1];
            Assert.AreEqual("St", secondPossibleCityDetails.CityName);
            Assert.AreEqual("Pierre", secondPossibleCityDetails.CountryName);
			thirdPossibleCityDetails = potentialCityDetails[2];
            Assert.AreEqual("St", thirdPossibleCityDetails.CityName);
            Assert.AreEqual("Pierre", thirdPossibleCityDetails.AdministrativeDivisionName);

			cityToken = new CityToken("Sydney, New South Wales, Australia", 0);
            potentialCityDetails = cityToken.GetPotentialCityDetails();
            Assert.AreEqual(1, cityToken.GetPotentialCityDetails().Count);
            firstPossibleCityDetails = potentialCityDetails.First();
            Assert.AreEqual("Sydney", firstPossibleCityDetails.CityName);
			Assert.AreEqual("New South Wales", firstPossibleCityDetails.AdministrativeDivisionName);
            Assert.AreEqual("Australia", firstPossibleCityDetails.CountryName);
        }

		[Test]
        public void GetPotentialCityDetails_should_handle_bad_positioned_commas()
        {
            var cityToken = new CityToken("Sydney,", 0);
            var potentialCityDetails = cityToken.GetPotentialCityDetails();
            Assert.AreEqual(1, cityToken.GetPotentialCityDetails().Count);
            var firstPossibleCityDetails = potentialCityDetails.First();
            Assert.AreEqual("Sydney", firstPossibleCityDetails.CityName);
            Assert.IsNull(firstPossibleCityDetails.CountryName);

			cityToken = new CityToken("Sydney, Australia,", 0);
            potentialCityDetails = cityToken.GetPotentialCityDetails();
            Assert.AreEqual(2, cityToken.GetPotentialCityDetails().Count);
            firstPossibleCityDetails = potentialCityDetails.First();
            Assert.AreEqual("Sydney", firstPossibleCityDetails.CityName);
            Assert.AreEqual("Australia", firstPossibleCityDetails.CountryName);
			var secondPossibleCityDetails = potentialCityDetails[1];
            Assert.AreEqual("Sydney", secondPossibleCityDetails.CityName);
            Assert.AreEqual("Australia", secondPossibleCityDetails.AdministrativeDivisionName);
		}
    }
}

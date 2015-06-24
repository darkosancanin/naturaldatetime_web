using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NaturalDateTime.Tests
{
    [TestFixture]
    public class CityTokenizer_Test 
    {
        [Test]
        public void CityTokenizer_should_find_all_occurrences_of_the_city()
        {
			var question = new Question("what time is it in Melbourne");
			var token = question.GetToken<CityToken>();
			Assert.AreEqual("Melbourne", token.Value);

			question = new Question("whats the time in New England, USA");
			token = question.GetToken<CityToken>();
			Assert.AreEqual("New England, USA", token.Value);

			question = new Question("when its the 24th of April at 8pm in Sydney what time is it in Bangladesh");
			token = question.GetToken<CityToken>();
			Assert.AreEqual("Sydney", token.Value);
			token = question.GetToken<CityToken>(2);
			Assert.AreEqual("Bangladesh", token.Value);
			
			question = new Question("whats the time at Mexico when its the 24th of April at 7PM in New York, USA");
			token = question.GetToken<CityToken>();
			Assert.AreEqual("Mexico", token.Value);
			token = question.GetToken<CityToken>(2);
			Assert.AreEqual("New York, USA", token.Value);

			question = new Question("how many days in a year");
            Assert.IsNull(question.GetToken<CityToken>());
			
			question = new Question("whats the time in the Phillipines");
			token = question.GetToken<CityToken>();
			Assert.AreEqual("Phillipines", token.Value);
			
			question = new Question("time in the new york right now");
			token = question.GetToken<CityToken>();
			Assert.AreEqual("new york", token.Value);

            question = new Question("time in the new york now");
			token = question.GetToken<CityToken>();
			Assert.AreEqual("new york", token.Value);
        }
    }
}

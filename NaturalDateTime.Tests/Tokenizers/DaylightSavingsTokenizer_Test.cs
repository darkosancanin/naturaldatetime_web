using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NaturalDateTime.Tests
{
    [TestFixture]
    public class DaylightSavingsTokenizer_Test
    {
        [Test]
        public void DaylightSavingsTokenizer_should_find_all_occurrences()
        {
			var question = new Question("when does daylight savings begin in New York");
            var token = question.GetToken<DaylightSavingsToken>();
			Assert.AreEqual("daylight savings", token.Value);
			
			question = new Question("when does daylight savings end in New York");
            token = question.GetToken<DaylightSavingsToken>();
			Assert.AreEqual("daylight savings", token.Value);
			
			question = new Question("when does daylight saving's end in New York");
            token = question.GetToken<DaylightSavingsToken>();
			Assert.AreEqual("daylight saving's", token.Value);
			
			question = new Question("when does day light savings end in New York");
            token = question.GetToken<DaylightSavingsToken>();
			Assert.AreEqual("day light savings", token.Value);
			
			question = new Question("when does day light saving's end in New York");
            token = question.GetToken<DaylightSavingsToken>();
			Assert.AreEqual("day light saving's", token.Value);
        }
    }
}

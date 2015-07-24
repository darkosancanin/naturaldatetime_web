using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NaturalDateTime.Tests
{
    [TestFixture]
    public class TimezoneTokenizer_Test
    {
        [Test]
        public void TimezoneTokenizer_should_find_all_occurrences()
        {
			var question = new Question("time in AEST");
            var token = question.GetToken<TimezoneToken>();
			Assert.AreEqual("AEST", token.Value);
			
			question = new Question("whats the time in pacific time");
			token = question.GetToken<TimezoneToken>();
			Assert.AreEqual("pacific time", token.Value);
			
			question = new Question("whats the time in ET if its 4pm in Sydney");
			token = question.GetToken<TimezoneToken>();
			Assert.AreEqual("ET", token.Value);
        }
    }
}

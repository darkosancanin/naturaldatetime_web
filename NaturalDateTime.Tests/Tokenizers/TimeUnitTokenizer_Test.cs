using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NaturalDateTime.Tests
{
    [TestFixture]
    public class TimeUnitTokenizer_Test
    {
        [Test]
        public void TimeUnitTokenizer_should_find_all_occurrences()
        {
			var question = new Question("how many days between Easter and");
            var token = question.GetToken<TimeUnitToken>();
			Assert.AreEqual("days", token.Value);
			
			question = new Question("how many days in a year");
            token = question.GetToken<TimeUnitToken>();
			Assert.AreEqual("days", token.Value);
			token = question.GetToken<TimeUnitToken>(2);
			Assert.AreEqual("year", token.Value);
			
			question = new Question("how many seconds in a day");
            token = question.GetToken<TimeUnitToken>();
			Assert.AreEqual("seconds", token.Value);
			token = question.GetToken<TimeUnitToken>(2);
			Assert.AreEqual("day", token.Value);
			
			question = new Question("how many minutes in a year");
			token = question.GetToken<TimeUnitToken>();
			Assert.AreEqual("minutes", token.Value);
			token = question.GetToken<TimeUnitToken>(2);
			Assert.AreEqual("year", token.Value);
        }
    }
}

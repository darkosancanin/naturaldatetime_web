using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NaturalDateTime.Tests
{
    [TestFixture]
    public class LiteralDateOrTimeToken_Test
    {
        [Test]
        public void LiteralDateOrTimeTokenizer_should_find_all_occurrences_of_time()
        {
			var question = new Question("what time is it");
            var token = question.GetToken<LiteralDateOrTimeToken>();
			Assert.AreEqual("time", token.Value);
			
			question = new Question("what's the time and another time here");
            token = question.GetToken<LiteralDateOrTimeToken>();
			Assert.AreEqual("time", token.Value);
			token = question.GetToken<LiteralDateOrTimeToken>(2);
			Assert.AreEqual("time", token.Value);

			question = new Question("time in new york");
            token = question.GetToken<LiteralDateOrTimeToken>();
			Assert.AreEqual("time", token.Value);
        }

		[Test]
        public void LiteralDateOrTimeTokenizer_should_find_all_occurrences_of_date()
        {
			var question = new Question("what date is it in vancouver");
            var token = question.GetToken<LiteralDateOrTimeToken>();
			Assert.AreEqual("date", token.Value);
			
			question = new Question("date in new york");
            token = question.GetToken<LiteralDateOrTimeToken>();
			Assert.AreEqual("date", token.Value);
        }
    }
}

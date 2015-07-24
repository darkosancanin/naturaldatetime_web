using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NaturalDateTime.Tests
{
    [TestFixture]
    public class WhatTimeInCityQuestionHandler_Test
    {
        [Test]
        public void WhatTimeInCityQuestionHandler_should_answer_basic_what_is_time()
        {
            var handler = new WhatTimeInCityQuestionHandler();

            var question = new Question("What is the time in New York City");
            Assert.IsTrue(handler.CanAnswerQuestion(question));

            question = new Question("Time in New York City");
            Assert.IsTrue(handler.CanAnswerQuestion(question));
        }
    }
}

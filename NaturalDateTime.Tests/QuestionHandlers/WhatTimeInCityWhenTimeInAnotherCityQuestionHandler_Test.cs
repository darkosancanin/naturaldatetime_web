using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NaturalDateTime.Tests
{
    [TestFixture]
    public class WhatTimeInCityWhenTimeInAnotherCityQuestionHandler_Test
    {
        [Test]
        public void WhatTimeInCityWhenTimeInAnotherCityQuestionHandler_should_answer_when_two_cities_and_a_date_are_provied()
        {
            var handler = new WhatTimeInCityWhenTimeInAnotherCityQuestionHandler();

            var question = new Question("What is the time in New York City when its 2pm in Sydney");
            Assert.IsTrue(handler.CanAnswerQuestion(question));

            question = new Question("If its 2pm in Boston whats the time in sydney");
            Assert.IsTrue(handler.CanAnswerQuestion(question));
        }

        [Test]
        public void WhatTimeInCityWhenTimeInAnotherCityQuestionHandler_should_answer_if_city_or_timezone_is_provied()
        {
            var handler = new WhatTimeInCityWhenTimeInAnotherCityQuestionHandler();

            var question = new Question("What is the time in Eastern Time when its 2pm in PT");
            Assert.IsTrue(handler.CanAnswerQuestion(question));

            question = new Question("If its 2pm in Boston whats the time in CET");
            Assert.IsTrue(handler.CanAnswerQuestion(question));
        }

        [Test]
        public void WhatTimeInCityWhenTimeInAnotherCityQuestionHandler_should_not_answer_if_only_one_city_or_tz_is_provided()
        {
            var handler = new WhatTimeInCityWhenTimeInAnotherCityQuestionHandler();

            var question = new Question("What is the time in New York City");
            Assert.IsFalse(handler.CanAnswerQuestion(question));

            question = new Question("Time in NYC");
            Assert.IsFalse(handler.CanAnswerQuestion(question));
        }
    }
}

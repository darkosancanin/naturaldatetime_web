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
        public void WhatTimeInCityQuestionHandler_should_answer_questions_if_no_tokens_are_found()
        {
			var question = new Question("New York City");
            var handler = new WhatTimeInCityQuestionHandler();
            Assert.AreEqual(0, question.Tokens.Count);
            Assert.IsTrue(handler.CanAnswerQuestion(question));
        }
    }
}

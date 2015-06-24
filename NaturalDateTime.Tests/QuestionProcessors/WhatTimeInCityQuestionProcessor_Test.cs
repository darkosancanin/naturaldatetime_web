using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NaturalDateTime.Tests
{
    [TestFixture]
    public class WhatTimeInCityQuestionProcessor_Test
    {
        [Test]
        public void WhatTimeInCityQuestionProcessor_should_answer_questions_if_no_tokens_are_found()
        {
			var question = new Question("New York City");
            var processor = new WhatTimeInCityQuestionProcessor();
            Assert.AreEqual(0, question.Tokens.Count);
            Assert.IsTrue(processor.CanAnswerQuestion(question));
        }
    }
}

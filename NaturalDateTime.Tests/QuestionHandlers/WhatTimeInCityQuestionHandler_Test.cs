using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NaturalDateTime.Tests
{
    [TestFixture]
    public class NoTokensQuestionHandler_Test
    {
        [Test]
        public void NoTokensQuestionHandler_should_answer_questions_if_no_tokens_are_found()
        {
			var question = new Question("New York City");
            var handler = new NoTokensQuestionHandler();
            Assert.AreEqual(0, question.Tokens.Count);
            Assert.IsTrue(handler.CanAnswerQuestion(question));
        }
    }
}

using NUnit.Framework;

namespace NaturalDateTime.Tests
{
    [TestFixture]
    public class Question_Test
    {
        [Test]
        public void PreProcessText_should_remove_multiple_whitespaces()
        {
            var question = new Question("this     is a       test");
            Assert.AreEqual("this is a test", question.QuestionText);
        }

		[Test]
        public void PreProcessText_should_remove_the_question_mark_from_the_end()
        {
            var question = new Question("this is a test?");
            Assert.AreEqual("this is a test", question.QuestionText);

			question = new Question("this is a test ? ");
            Assert.AreEqual("this is a test", question.QuestionText);
        }
    }
}

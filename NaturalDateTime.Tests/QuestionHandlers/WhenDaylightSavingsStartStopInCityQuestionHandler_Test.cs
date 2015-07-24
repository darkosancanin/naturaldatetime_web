using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NaturalDateTime.Tests
{
    [TestFixture]
    public class WhenDaylightSavingsStartStopInCityQuestionHandler_Test
    {
        [Test]
        public void WhenDaylightSavingsStartStopInCityQuestionHandler_should_answer_questions_if_daylight_saving_token_is_found()
        {
			var question = new Question("When does daylight savings start in New York.");
            var handler = new WhenDaylightSavingInCityQuestionHandler();
            Assert.IsNotNull(question.GetToken<DaylightSavingsToken>());
            Assert.IsTrue(handler.CanAnswerQuestion(question));
        }

        [Test]
        public void WhenDaylightSavingsStartStopInCityQuestionHandler_should_answer_question_only_if_a_city_token_is_found()
        {
            var question = new Question("When does daylight savings start");
            var handler = new WhenDaylightSavingInCityQuestionHandler();
            Assert.IsNull(question.GetToken<CityToken>());
            Assert.IsFalse(handler.CanAnswerQuestion(question));
        }

        [Test]
        public void WhenDaylightSavingsStartStopInCityQuestionHandler_should_answer_when_asking_for_dst()
        {
            var question = new Question("DST time in London");
            var handler = new WhenDaylightSavingInCityQuestionHandler();
            Assert.IsNotNull(question.GetToken<DaylightSavingsToken>());
            Assert.IsTrue(handler.CanAnswerQuestion(question));
        }
    }
}

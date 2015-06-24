using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NaturalDateTime.Tests
{
    [TestFixture]
    public class WhenDaylightSavingsStartStopInCityProcessor_Test
    {
        [Test]
        public void WhenDaylightSavingsStartStopInCityProcessor_should_answer_questions_if_daylight_saving_token_is_found()
        {
			var question = new Question("When does daylight savings start in New York.");
            var processor = new WhenDaylightSavingInCityProcessor();
            Assert.IsNotNull(question.GetToken<DaylightSavingsToken>());
            Assert.IsTrue(processor.CanAnswerQuestion(question));
        }

        [Test]
        public void WhenDaylightSavingsStartStopInCityProcessor_should_answer_question_only_if_a_city_token_is_found()
        {
            var question = new Question("When does daylight savings start");
            var processor = new WhenDaylightSavingInCityProcessor();
            Assert.IsNull(question.GetToken<CityToken>());
            Assert.IsFalse(processor.CanAnswerQuestion(question));
        }
    }
}

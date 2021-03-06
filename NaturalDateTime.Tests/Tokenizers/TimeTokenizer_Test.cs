﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NaturalDateTime.Tests
{
    [TestFixture]
    public class TimeTokenizer_Test
    {
        [Test]
        public void TimeTokenizer_should_find_all_occurrences_of_known_formats()
        {
			var question = new Question("when its 7am in Sydney");
            var token = question.GetToken<TimeToken>();
			Assert.AreEqual("7am", token.Value);
			Assert.AreEqual (7, token.Hour);
			Assert.AreEqual (null, token.Minute);
			Assert.AreEqual (Meridiem.AM, token.Meridiem);
			
			question = new Question("when its 7oclock in Sydney");
			token = question.GetToken<TimeToken>();
			Assert.AreEqual("7oclock", token.Value);
			Assert.AreEqual (7, token.Hour);
			Assert.AreEqual (null, token.Minute);
			Assert.AreEqual (Meridiem.NONE, token.Meridiem);
			
			question = new Question("when its 7 o'clock in Sydney");
			token = question.GetToken<TimeToken>();
			Assert.AreEqual("7 o'clock", token.Value);
			Assert.AreEqual (7, token.Hour);
			Assert.AreEqual (null, token.Minute);
			Assert.AreEqual (Meridiem.NONE, token.Meridiem);
			
			question = new Question("when its 7 pm in Sydney");
            token = question.GetToken<TimeToken>();
			Assert.AreEqual("7 pm", token.Value);
			Assert.AreEqual (7, token.Hour);
			Assert.AreEqual (null, token.Minute);
			Assert.AreEqual (Meridiem.PM, token.Meridiem);

            question = new Question("when its 7 p.m in Sydney");
            token = question.GetToken<TimeToken>();
            Assert.AreEqual("7 p.m", token.Value);
            Assert.AreEqual(7, token.Hour);
            Assert.AreEqual(null, token.Minute);
            Assert.AreEqual(Meridiem.PM, token.Meridiem);

            question = new Question("when its 7 a.m in Sydney");
            token = question.GetToken<TimeToken>();
            Assert.AreEqual("7 a.m", token.Value);
            Assert.AreEqual(7, token.Hour);
            Assert.AreEqual(null, token.Minute);
            Assert.AreEqual(Meridiem.AM, token.Meridiem);

            question = new Question("when its 7 in Sydney");
            token = question.GetToken<TimeToken>();
			Assert.AreEqual("7", token.Value);
			Assert.AreEqual (7, token.Hour);
			Assert.AreEqual (null, token.Minute);
			Assert.AreEqual (Meridiem.NONE, token.Meridiem);
			
			question = new Question("when its 7:30am in Sydney");
			token = question.GetToken<TimeToken>();
			Assert.AreEqual("7:30am", token.Value);
			Assert.AreEqual (7, token.Hour);
			Assert.AreEqual (30, token.Minute);
			Assert.AreEqual (Meridiem.AM, token.Meridiem);
			
			question = new Question("when its 7:30 pm in Sydney");
			token = question.GetToken<TimeToken>();
			Assert.AreEqual("7:30 pm", token.Value);
			Assert.AreEqual (7, token.Hour);
			Assert.AreEqual (30, token.Minute);
			Assert.AreEqual (Meridiem.PM, token.Meridiem);
			
			question = new Question("when its 10:22 in Sydney");
			token = question.GetToken<TimeToken>();
			Assert.AreEqual("10:22", token.Value);
			Assert.AreEqual (10, token.Hour);
			Assert.AreEqual (22, token.Minute);
			Assert.AreEqual (Meridiem.NONE, token.Meridiem);
			
			question = new Question("if it is 9.00 am in india what will be the time in macau");
			token = question.GetToken<TimeToken>();
			Assert.AreEqual("9.00 am", token.Value);
			Assert.AreEqual (9, token.Hour);
			Assert.AreEqual (0, token.Minute);
			Assert.AreEqual (Meridiem.AM, token.Meridiem);
			
			question = new Question("on the 24th of April 2012");
			Assert.IsNull (question.GetToken<TimeToken>());
        }

        [Test]
        public void TimeTokenizer_should_find_all_occurrences_of_abbreviations()
        {
            var question = new Question("when its noon in Sydney");
            var token = question.GetToken<TimeToken>();
            Assert.AreEqual("noon", token.Value);
            Assert.AreEqual(12, token.Hour);
            Assert.AreEqual(null, token.Minute);
            Assert.AreEqual(Meridiem.PM, token.Meridiem);

            question = new Question("when its midday in Sydney");
            token = question.GetToken<TimeToken>();
            Assert.AreEqual("midday", token.Value);
            Assert.AreEqual(12, token.Hour);
            Assert.AreEqual(null, token.Minute);
            Assert.AreEqual(Meridiem.PM, token.Meridiem);

            question = new Question("when its mid-day  in Sydney");
            token = question.GetToken<TimeToken>();
            Assert.AreEqual("mid-day", token.Value);
            Assert.AreEqual(12, token.Hour);
            Assert.AreEqual(null, token.Minute);
            Assert.AreEqual(Meridiem.PM, token.Meridiem);

            question = new Question("when its mid day in Sydney");
            token = question.GetToken<TimeToken>();
            Assert.AreEqual("mid day", token.Value);
            Assert.AreEqual(12, token.Hour);
            Assert.AreEqual(null, token.Minute);
            Assert.AreEqual(Meridiem.PM, token.Meridiem);

            question = new Question("when its midnight in Sydney");
            token = question.GetToken<TimeToken>();
            Assert.AreEqual("midnight", token.Value);
            Assert.AreEqual(12, token.Hour);
            Assert.AreEqual(null, token.Minute);
            Assert.AreEqual(Meridiem.AM, token.Meridiem);

            question = new Question("when its mid-night  in Sydney");
            token = question.GetToken<TimeToken>();
            Assert.AreEqual("mid-night", token.Value);
            Assert.AreEqual(12, token.Hour);
            Assert.AreEqual(null, token.Minute);
            Assert.AreEqual(Meridiem.AM, token.Meridiem);

            question = new Question("when its mid night in Sydney");
            token = question.GetToken<TimeToken>();
            Assert.AreEqual("mid night", token.Value);
            Assert.AreEqual(12, token.Hour);
            Assert.AreEqual(null, token.Minute);
            Assert.AreEqual(Meridiem.AM, token.Meridiem);
        }
    }
}

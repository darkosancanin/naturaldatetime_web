using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NaturalDateTime.Tests
{
    [TestFixture]
    public class DateTokenizer_Test
    {
		[Test]
        public void DateTokenizer_should_find_all_occurrences_of_year()
        {
			var question = new Question("how long until 2012");
            var token = question.GetToken<DateToken>();
			Assert.AreEqual("2012", token.Value);
			Assert.AreEqual (null, token.Day);
			Assert.AreEqual (null, token.Month);
			Assert.AreEqual (2012, token.Year);
			
			question = new Question("its 2013 in Sydney");
			token = question.GetToken<DateToken>();
			Assert.AreEqual("2013", token.Value);
			Assert.AreEqual (null, token.Day);
			Assert.AreEqual (null, token.Month);
			Assert.AreEqual (2013, token.Year);
        }
		
		[Test]
        public void DateTokenizer_should_find_all_occurrences_of_month_day_year_dates()
        {
			var question = new Question("its april 24 in Sydney");
            var token = question.GetToken<DateToken>();
			Assert.AreEqual("april 24", token.Value);
			Assert.AreEqual (24, token.Day);
			Assert.AreEqual (4, token.Month);
			Assert.AreEqual (null, token.Year);
			
			question = new Question("its april 5 in Sydney");
			token = question.GetToken<DateToken>();
			Assert.AreEqual("april 5", token.Value);
			Assert.AreEqual (5, token.Day);
			Assert.AreEqual (4, token.Month);
			Assert.AreEqual (null, token.Year);
				
			question = new Question("its april 2012 in Sydney");
			token = question.GetToken<DateToken>();
			Assert.AreEqual("april 2012", token.Value);
			Assert.AreEqual (null, token.Day);
			Assert.AreEqual (4, token.Month);
			Assert.AreEqual (2012, token.Year);
			
			question = new Question("its april, 2012 in Sydney");
			token = question.GetToken<DateToken>();
			Assert.AreEqual("april, 2012", token.Value);
			Assert.AreEqual (null, token.Day);
			Assert.AreEqual (4, token.Month);
			Assert.AreEqual (2012, token.Year);
			
			question = new Question("its april 13 2012 in Sydney");
            token = question.GetToken<DateToken>();
			Assert.AreEqual("april 13 2012", token.Value);
			Assert.AreEqual (13, token.Day);
			Assert.AreEqual (4, token.Month);
			Assert.AreEqual (2012, token.Year);
			
			question = new Question("its april 3 2012 in Sydney");
			token = question.GetToken<DateToken>();
			Assert.AreEqual("april 3 2012", token.Value);
			Assert.AreEqual (3, token.Day);
			Assert.AreEqual (4, token.Month);
			Assert.AreEqual (2012, token.Year);
			
			question = new Question("its april the 24th 2012 in Sydney");
			token = question.GetToken<DateToken>();
			Assert.AreEqual("april the 24th 2012", token.Value);
			Assert.AreEqual (24, token.Day);
			Assert.AreEqual (4, token.Month);
			Assert.AreEqual (2012, token.Year);
        }
		
		[Test]
        public void DateTokenizer_should_find_all_occurrences_of_day_month_year_dates()
        {
			var question = new Question("its 24-apr-12 in Sydney");
            var token = question.GetToken<DateToken>();
			Assert.AreEqual("24-apr-12", token.Value);
			Assert.AreEqual (24, token.Day);
			Assert.AreEqual (4, token.Month);
			Assert.AreEqual (2012, token.Year);
			
			question = new Question("its 24-apr-2012 in Sydney");
			token = question.GetToken<DateToken>();
			Assert.AreEqual("24-apr-2012", token.Value);
			Assert.AreEqual (24, token.Day);
			Assert.AreEqual (4, token.Month);
			Assert.AreEqual (2012, token.Year);
				
			question = new Question("its 24th of april 2012 in Sydney");
			token = question.GetToken<DateToken>();
			Assert.AreEqual("24th of april 2012", token.Value);
			Assert.AreEqual (24, token.Day);
			Assert.AreEqual (4, token.Month);
			Assert.AreEqual (2012, token.Year);
			
			question = new Question("its 24th april in Sydney");
			token = question.GetToken<DateToken>();
			Assert.AreEqual("24th april", token.Value);
			Assert.AreEqual (24, token.Day);
			Assert.AreEqual (4, token.Month);
			Assert.AreEqual (null, token.Year);
			
			question = new Question("its 24 april in Sydney");
            token = question.GetToken<DateToken>();
			Assert.AreEqual("24 april", token.Value);
			Assert.AreEqual (24, token.Day);
			Assert.AreEqual (4, token.Month);
			Assert.AreEqual (null, token.Year);
        }
		
        [Test]
        public void DateTokenizer_should_find_all_occurrences_of_hyphenated_dates()
        {
			var question = new Question("its 4/24/2012 in Sydney");
            var token = question.GetToken<DateToken>();
			Assert.AreEqual("4/24/2012", token.Value);
			Assert.AreEqual (24, token.Day);
			Assert.AreEqual (4, token.Month);
			Assert.AreEqual (2012, token.Year);
			
			question = new Question("its 4/24/12 in Sydney");
			token = question.GetToken<DateToken>();
			Assert.AreEqual("4/24/12", token.Value);
			Assert.AreEqual (24, token.Day);
			Assert.AreEqual (4, token.Month);
			Assert.AreEqual (2012, token.Year);
			
			question = new Question("its 4-24-12 in Sydney");
			token = question.GetToken<DateToken>();
			Assert.AreEqual("4-24-12", token.Value);
			Assert.AreEqual (24, token.Day);
			Assert.AreEqual (4, token.Month);
			Assert.AreEqual (2012, token.Year);
			
			question = new Question("its 4-24-2012 in Sydney");
			token = question.GetToken<DateToken>();
			Assert.AreEqual("4-24-2012", token.Value);
			Assert.AreEqual (24, token.Day);
			Assert.AreEqual (4, token.Month);
			Assert.AreEqual (2012, token.Year);
			
			question = new Question("its 24-4-2012 in Sydney");
			token = question.GetToken<DateToken>();
			Assert.AreEqual("24-4-2012", token.Value);
			Assert.AreEqual (24, token.Day);
			Assert.AreEqual (4, token.Month);
			Assert.AreEqual (2012, token.Year);
			
			question = new Question("its 24/4/2012 in Sydney");
			token = question.GetToken<DateToken>();
			Assert.AreEqual("24/4/2012", token.Value);
			Assert.AreEqual (24, token.Day);
			Assert.AreEqual (4, token.Month);
			Assert.AreEqual (2012, token.Year);
			
			question = new Question("its 24/4/12 in Sydney");
			token = question.GetToken<DateToken>();
			Assert.AreEqual("24/4/12", token.Value);
			Assert.AreEqual (24, token.Day);
			Assert.AreEqual (4, token.Month);
			Assert.AreEqual (2012, token.Year);
        }
    }
}

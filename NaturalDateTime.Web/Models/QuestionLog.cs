using System;
using NodaTime;
using System.Globalization;
using System.ComponentModel.DataAnnotations.Schema;

namespace NaturalDateTime.Web.Models
{
	public class QuestionLog
	{
		public int Id { get; set; }
		public string AnswerText { get; set; }
		public string Question { get; set; }
		public string Note { get; set; }
		public DateTime UtcTime { get;set; }
        public bool UnderstoodQuestion { get; set; }
		public bool AnsweredQuestion { get; set; }
		public string Client { get; set; }
		public string Version { get; set; }
		public bool IsBot { get; set; }

        public QuestionLog() { }
		public QuestionLog(Question question, Answer answer, DateTime utcTime, string client, string version, bool isBot)
		{
			Question = question.QuestionText;
			AnswerText = answer.AnswerText;
            UtcTime = utcTime;
			Note = answer.Note;
			UnderstoodQuestion = answer.UnderstoodQuestion;
			AnsweredQuestion = answer.AnsweredQuestion;
			IsBot = isBot;
			Client = client;
			string fullVersion = null;
			if(version != null) fullVersion = version.Replace("_", ".");
			Version = fullVersion;
		}

        [NotMapped]
        public string SydneyTime
        {
            get
            {
                var sydneyTime = new DateTimeManager().ConvertDateTime(LocalDateTime.FromDateTime(UtcTime), DateTimeZone.Utc, DateTimeZoneProviders.Tzdb["Australia/Sydney"]);
                return sydneyTime.ConvertedZonedDateTime.LocalDateTime.ToString("ddd, dd MMM yyyy h:mm tt", CultureInfo.InvariantCulture).ToString();

            }
        }
    }
}


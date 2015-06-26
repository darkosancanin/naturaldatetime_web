﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NaturalDateTime.Web.Models;
using NaturalDateTime.Web.DataAccess;
using System.Diagnostics;

namespace NaturalDateTime.Web.Services
{
    public class QuestionService
    {
        private NaturalDateTimeContext dbContext = new NaturalDateTimeContext();

        public Answer GetAnswer(string questionText, string userAgent, string client, string client_version)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var question = new Question(questionText);
            var questionProcessorResolver = new QuestionProcessorResolver();
            var questionProcessor = questionProcessorResolver.ResolveOrNull(question);
            Answer answer;
            if (questionProcessor != null)
            {
                answer = questionProcessor.GetAnswer(question);
            }
            else
            {
                answer = new Answer(question, false, false, ErrorMessages.DidNotUnderstandQuestion);
            }
            answer.AddDebugInformation("Processing Time", String.Format("{0} ms", stopWatch.ElapsedMilliseconds.ToString()));
            answer.AddDebugInformation("Tokens", answer.Question.FormatTextWithTokens());

            if (string.IsNullOrEmpty(client)) client = ApplicationSettings.WebApplicationName;
            if (string.IsNullOrEmpty(client_version)) client_version = ApplicationSettings.CurrentWebVersion;
            var questionLog = new QuestionLog(question, answer, DateTime.UtcNow, client, client_version, IsBot(userAgent));
            dbContext.QuestionLog.Add(questionLog);
            dbContext.SaveChanges();

            return answer;
        }
        public Answer GetAnswer(string questionText, string userAgent)
        {
            return GetAnswer(questionText, userAgent, ApplicationSettings.WebApplicationName, ApplicationSettings.CurrentWebVersion);
        }

        private bool IsBot(string userAgent)
        {
            var isBot = userAgent != null && userAgent.ToLower().Contains("bot");
            return isBot;
        }
    }
}
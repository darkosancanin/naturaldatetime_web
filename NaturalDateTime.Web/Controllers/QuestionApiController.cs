using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NaturalDateTime.Web.Models;
using NaturalDateTime.Web.DataAccess;
using System.Diagnostics;

namespace NaturalDateTime.Web.Controllers
{
    public class QuestionApiController : ApiController
    {
        private NaturalDateTimeContext dbContext = new NaturalDateTimeContext();

        public IHttpActionResult Get(string q)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var question = new Question(q);
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

            var questionLog = new QuestionLog(question, answer, DateTime.UtcNow, ApplicationSettings.WebApplicationName, ApplicationSettings.CurrentWebVersion, IsBot());
            dbContext.QuestionLog.Add(questionLog);
            dbContext.SaveChanges();

            var answerModel = new AnswerModel(answer);
            
            return Ok(new AnswerModel(answer));
        }

        private bool IsBot()
        {
            var isBot = Request.Headers.UserAgent != null && Request.Headers.UserAgent.ToString().ToLower().Contains("bot");
            return isBot;
        }
    }
}
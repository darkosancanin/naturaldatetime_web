using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using NaturalDateTime.Web.Models;
using System.Web.Mvc;
using System.Web;
using NaturalDateTime.Web.DataAccess;
using NaturalDateTime.Services;

namespace NaturalDateTime.Web.Controllers
{
    public class ApiController : Controller
    {
        [HttpPost]
        public ActionResult Question(string question, string client, string client_version, string debug)
        {
            if (string.IsNullOrEmpty(question)) throw new HttpException(400, "Invalid request. Question not specified.");
            if (string.IsNullOrEmpty(client)) throw new HttpException(400, "Invalid request. Client not specified.");
            if (string.IsNullOrEmpty(client_version)) throw new HttpException(400, "Invalid request. Client version not specified.");

            question = HttpUtility.UrlDecode(question);
            var userAgent = String.Empty;
            if (Request.Headers["User-Agent"] != null)
                userAgent = Request.Headers["User-Agent"].ToString();
            var answerService = new AnswerService();
            var answer = answerService.GetAnswer(question, !String.IsNullOrEmpty(debug));

            var dbContext = new NaturalDateTimeContext();
            var questionLog = new QuestionLog(answer.Question, answer, DateTime.UtcNow, client, client_version, IsBot(userAgent));
            dbContext.AddQuestionLog(questionLog);
            dbContext.SaveChanges();

            return Json(new AnswerModel(answer), JsonRequestBehavior.AllowGet);
        }
        private bool IsBot(string userAgent)
        {
            var isBot = userAgent != null && userAgent.ToLower().Contains("bot");
            return isBot;
        }
    }
}
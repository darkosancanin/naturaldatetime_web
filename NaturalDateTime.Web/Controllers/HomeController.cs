using NaturalDateTime.Services;
using NaturalDateTime.Web.DataAccess;
using NaturalDateTime.Web.Models;
using NaturalDateTime.Web.Models.ViewModels;
using System;
using System.Web;
using System.Web.Mvc;

namespace NaturalDateTime.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string q, string client, string client_version, string debug)
        {
            ViewBag.Title = "Natural Date and Time";
            if (!string.IsNullOrEmpty(q))
            {
                q = HttpUtility.UrlDecode(q);
                q = q.Replace("_", " ");
                ViewBag.Title = q + " -  Natural Date and Time";
                
                var answerService = new AnswerService();
                var answer = answerService.GetAnswer(q);

                var userAgent = String.Empty;
                if (Request.Headers["User-Agent"] != null)
                    userAgent = Request.Headers["User-Agent"].ToString();
                if (string.IsNullOrEmpty(client)) client = "web";
                if (string.IsNullOrEmpty(client_version)) client_version = "2.0";
                var dbContext = new NaturalDateTimeContext();
                var questionLog = new QuestionLog(answer.Question, answer, DateTime.UtcNow, client, client_version, IsBot(userAgent));
                dbContext.AddQuestionLog(questionLog);
                dbContext.SaveChanges();

                return View("Index", new HomeViewModel(q, answer.AnswerText, answer.Note));
            }

            return View(new HomeViewModel(!String.IsNullOrEmpty(debug)));
        }

        private bool IsBot(string userAgent)
        {
            var isBot = userAgent != null && userAgent.ToLower().Contains("bot");
            return isBot;
        }
    }
}

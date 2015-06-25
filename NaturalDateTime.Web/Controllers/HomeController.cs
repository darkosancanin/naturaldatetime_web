using NaturalDateTime.Web.Models.ViewModels;
using NaturalDateTime.Web.Services;
using System;
using System.Web;
using System.Web.Mvc;

namespace NaturalDateTime.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string debug)
        {
            ViewBag.Title = "Natural Date and Time";

            return View(new HomeViewModel(!String.IsNullOrEmpty(debug)));
        }

        public ActionResult Question(string questionText)
        {
            questionText = HttpUtility.UrlDecode(questionText);
            questionText = questionText.Replace("_", " ");
            ViewBag.Title = questionText + " -  Natural Date and Time";
            var userAgent = String.Empty;
            if (Request.Headers["User-Agent"] != null)
                userAgent = Request.Headers["User-Agent"].ToString();
            var questionService = new QuestionService();
            var answer = questionService.GetAnswer(questionText, userAgent);
            return View("Index", new HomeViewModel(questionText, answer.AnswerText, answer.Note));
        }
    }
}

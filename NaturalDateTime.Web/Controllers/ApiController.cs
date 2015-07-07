using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using NaturalDateTime.Web.Models;
using NaturalDateTime.Web.Services;
using System.Web.Mvc;
using System.Web;

namespace NaturalDateTime.Web.Controllers
{
    public class ApiController : Controller
    {
        [HttpPost]
        public ActionResult Question(string question, string client, string client_version)
        {
            if (string.IsNullOrEmpty(question)) throw new HttpException(400, "Invalid request. Question not specified.");
            if (string.IsNullOrEmpty(client)) throw new HttpException(400, "Invalid request. Client not specified.");
            if (string.IsNullOrEmpty(client_version)) throw new HttpException(400, "Invalid request. Client version not specified.");

            question = HttpUtility.UrlDecode(question);
            var userAgent = String.Empty;
            if (Request.Headers["User-Agent"] != null)
                userAgent = Request.Headers["User-Agent"].ToString();
            var questionService = new QuestionService();
            var answer = questionService.GetAnswer(question, userAgent, client, client_version);
            return Json(new AnswerModel(answer), JsonRequestBehavior.AllowGet);
        }
    }
}
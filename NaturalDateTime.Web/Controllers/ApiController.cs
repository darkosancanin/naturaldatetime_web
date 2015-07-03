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
        public ActionResult Question(string q, string client, string client_version)
        {
            if (string.IsNullOrEmpty(q)) throw new HttpException(400, "Invalid request. Question not specified.");
            if (string.IsNullOrEmpty(client)) throw new HttpException(400, "Invalid request. Client not specified.");
            if (string.IsNullOrEmpty(client_version)) throw new HttpException(400, "Invalid request. Client version not specified.");

            q = HttpUtility.UrlDecode(q);
            q = q.Replace("_", " ");
            var userAgent = String.Empty;
            if (Request.Headers["User-Agent"] != null)
                userAgent = Request.Headers["User-Agent"].ToString();
            var questionService = new QuestionService();
            var answer = questionService.GetAnswer(q, userAgent, client, client_version);
            return Json(new AnswerModel(answer), JsonRequestBehavior.AllowGet);
        }
    }
}
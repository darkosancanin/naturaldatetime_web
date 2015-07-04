﻿using NaturalDateTime.Web.Models.ViewModels;
using NaturalDateTime.Web.Services;
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
                var userAgent = String.Empty;
                if (Request.Headers["User-Agent"] != null)
                    userAgent = Request.Headers["User-Agent"].ToString();
                var questionService = new QuestionService();
                if (string.IsNullOrEmpty(client)) client = ApplicationSettings.WebClientName;
                if (string.IsNullOrEmpty(client_version)) client_version = ApplicationSettings.WebApplicationVersion;
                var answer = questionService.GetAnswer(q, userAgent, client, client_version);
                return View("Index", new HomeViewModel(q, answer.AnswerText, answer.Note));
            }

            return View(new HomeViewModel(!String.IsNullOrEmpty(debug)));
        }
    }
}

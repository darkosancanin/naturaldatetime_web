using Microsoft.AspNet.Identity;
using NaturalDateTime.Web.DataAccess;
using NaturalDateTime.Web.Models;
using NaturalDateTime.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace NaturalDateTime.Web.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            if(User.Identity.IsAuthenticated)
                return RedirectToAction("QuestionLogs");
            ViewBag.Title = "Administration - Natural Date and Time";
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginViewModel());
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (model.Password == System.Configuration.ConfigurationManager.AppSettings["AdminPassword"])
            {
                var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;
                authManager.SignIn(identity);
                if (Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("QuestionLogs");
            }
            else
            {
                throw new ApplicationException(string.Format("Invalid password attempt: '{0}' from {1}.", model.Password, Request.ServerVariables["REMOTE_ADDR"]));
            }
        }

        [Authorize]
        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;
            authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult QuestionLogs()
        {
            ViewBag.Title = "Question Logs - Natural Date and Time";
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult QuestionLogEntries(int page, int pageSize, bool showBotRequests)
        {
            var dbContext = new NaturalDateTimeContext();
            IQueryable<QuestionLog> questionsLogQuery = dbContext.QuestionLog;
            if(!showBotRequests)
                questionsLogQuery = questionsLogQuery.Where(x => !x.IsBot).AsQueryable();
            var total = questionsLogQuery.Count();
            var questionLogs = questionsLogQuery.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var questionLogResultSet = new QuestionLogResultSet(total, questionLogs);
            return Json(questionLogResultSet, JsonRequestBehavior.AllowGet);
        }
    }
}

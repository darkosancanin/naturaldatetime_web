using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NaturalDateTime.Web.Controllers
{
    public class ExamplesController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Examples - Natural Date and Time";
            return View();
        }
    }
}

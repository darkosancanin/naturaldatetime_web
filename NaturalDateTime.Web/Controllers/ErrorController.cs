﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NaturalDateTime.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Oops";
            return View();
        }
    }
}

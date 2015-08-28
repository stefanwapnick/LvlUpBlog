using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog.Controllers
{
    public class ErrorsController : Controller
    {
        public ActionResult FourHundredError()
        {
            return View();
        }
        public ActionResult FiveHundreadError()
        {
            return View();
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace STLTapReport.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /STLTapReport/

        public ActionResult Index()
        {
            ViewBag.Message = "Hello World!";
            return View();
        }

        public ActionResult Beers()
        {
            return View();
        }

        public ActionResult Styles()
        {
            return View();
        }
    }
}

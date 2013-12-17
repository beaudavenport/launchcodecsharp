using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STLTapReport.Models;

namespace STLTapReport.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /STLTapReport/

        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult Beers()
        {
            //LoginBoxModel model = new LoginBoxModel();
            //model.LoggedIn = Session["logged_in"] == null ? false : (bool)Session["logged_in"];
            //model.Name = model.LoggedIn ? (string)Session["name"] : "";
            return View();
        }

        public ActionResult Styles()
        {
            return View();
        }
    }
}
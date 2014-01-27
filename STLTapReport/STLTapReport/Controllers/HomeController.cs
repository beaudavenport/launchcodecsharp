using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STLTapReport.Models;
using STLTapReport.data;

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
            STLTapReportEntities context = new STLTapReportEntities();
            var model = context.beers.First();
            return View(model);
        }

        public ActionResult Styles()
        {
            BeerStyleModel model = new BeerStyleModel();
            STLTapReportEntities context = new STLTapReportEntities();
            model.styles = context.styles.ToList();
            return View(model);
        }
    }
}
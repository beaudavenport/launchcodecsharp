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
            BeersModel model = new BeersModel();
            model.beers = context.beers.ToList();
            return View(model);
        }

        public ActionResult Styles()
        {
            BeerStyleModel model = new BeerStyleModel();
            STLTapReportEntities context = new STLTapReportEntities();
            model.styles = context.styles.ToList();
            return View(model);
        }

        public ActionResult BeerProfile(int BeerProfileID)
        {
            STLTapReportEntities context = new STLTapReportEntities();
            beer model = new beer();
            model = context.beers.Where(x => x.beerID == BeerProfileID).SingleOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult BeerProfile(beer model)
        {
            STLTapReportEntities context = new STLTapReportEntities();

            //Get beer selected from profile page, then identify and get user that matches login ID
            beer beermodel = context.beers.Where(x => x.beerID == model.beerID).SingleOrDefault();
            int thisuserID = (int)Session["UserID"];
            user thisuser = context.users.Where(x => x.userID == thisuserID).SingleOrDefault();

            //Add the beer from the profile to the user's beers list (populates junction table)
            context.users.Attach(thisuser);
            var user = context.Entry(thisuser);
            user.Collection(x => x.beers).CurrentValue.Add(beermodel);

            //Make sure database knows to update and save changes
            context.Entry(thisuser).State = System.Data.Entity.EntityState.Unchanged;
            context.SaveChanges();

            return RedirectToAction("Beers");
         }
    }
}
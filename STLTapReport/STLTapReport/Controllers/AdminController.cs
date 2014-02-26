using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STLTapReport.data;
using STLTapReport.Models;

namespace STLTapReport.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult AdminWelcome()
        {
            if ((Session["is_Admin"] == null) || (!(bool)Session["is_Admin"]))
                return RedirectToAction("Welcome", "Home");

            return View();
        }

        public ActionResult AddBeer()
        {
            if ((Session["is_Admin"] == null) || (!(bool)Session["is_Admin"]))
                return RedirectToAction("Welcome", "Home");

            STLTapReportEntities context = new STLTapReportEntities();

            var model = new BeerAddModel();
            //Populate drop down list for view
            model.stylelist = new SelectList(context.styles.Select(x => new { x.name, x.styleID }), "styleID", "name");

            return View(model);

        }

        [HttpPost]
        public ActionResult AddBeer(BeerAddModel model)
        {
            if ((Session["is_Admin"] == null) || (!(bool)Session["is_Admin"]))
                return RedirectToAction("Welcome", "Home");

            STLTapReportEntities context = new STLTapReportEntities();
            model.stylelist = new SelectList(context.styles.Select(x => new { x.name, x.styleID }), "styleID", "name");
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {

                //Collect data from view model and add to a model of type beer that can be used to update the database
                var updatemodel = new beer();

                updatemodel.name = model.name;
                updatemodel.description = model.description;
                updatemodel.abv = model.abv;
                updatemodel.brewerylink = model.brewerylink;
                updatemodel.styleID = model.styleID;
                updatemodel.imageurl = model.imageurl;
                context.beers.Add(updatemodel);
                context.SaveChanges();

                return View("BeerAdded");
            }
   
        }

        public ActionResult RemoveBeer()
        {
            if ((Session["is_Admin"] == null) || (!(bool)Session["is_Admin"]))
                return RedirectToAction("Welcome", "Home");

            STLTapReportEntities context = new STLTapReportEntities();
            BeerRemoveModel model = new BeerRemoveModel();
           
            var NamesOfBeers = context.beers.Select(x => x.name).ToList();
            
            // Initiliaze checkbox to unchecked
            bool temp = false;
            model.BeersToRemove = new List<BeerToRemove>();

            // Assign name of each beer to an object of type BeerToRemove() 
            // which has only BeerName and IsChecked (false by default) properties, view doesn't need entire beer model

            foreach (string i in NamesOfBeers)
            {
                BeerToRemove thisbeer = new BeerToRemove();
                thisbeer.BeerName = i;
                thisbeer.IsChecked = temp;
                model.BeersToRemove.Add(thisbeer);     
            }
            
            return View(model);

        }

        [HttpPost]
        public ActionResult RemoveBeer(BeerRemoveModel model)
        {
            if ((Session["is_Admin"] == null) || (!(bool)Session["is_Admin"]))
                return RedirectToAction("Welcome", "Home");

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                
                STLTapReportEntities context = new STLTapReportEntities();

                //Check each beer for checked or not, if checked, remove beer with corresponding name from database.
                for (var i = 0; i < model.BeersToRemove.Count; i++)
                {

                    if (model.BeersToRemove[i].IsChecked == true)
                    {
                        string temp = model.BeersToRemove[i].BeerName;
                        beer beerToDrop = context.beers.Where(x => x.name == temp).SingleOrDefault();
                        
                        // make beerToDrop open to change
                        context.beers.Attach(beerToDrop);
                        var dbBeer = context.Entry(beerToDrop);

                        // strip beerToDrop of all associated users (i.e., junction table entry)
                        foreach (user m in beerToDrop.users.ToList())
                        {
                            dbBeer.Collection(y => y.users).CurrentValue.Remove(m);
                        }

                        context.Entry(beerToDrop).State = System.Data.Entity.EntityState.Unchanged;

                        // once user associations are removed, delete beer
                        context.beers.Remove(beerToDrop);
                    }
                }
                context.SaveChanges();
                return View("BeerRemoved");
            }
         
  
        }

    }
}

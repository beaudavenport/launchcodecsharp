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

        // Main page links
        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult Beers()
        {
            STLTapReportEntities context = new STLTapReportEntities();
            BeerStyleModel model = new BeerStyleModel();
            model.styles = context.styles.ToList();
            return View(model);
        }

        public ActionResult Styles()
        {
            BeerStyleModel model = new BeerStyleModel();
            STLTapReportEntities context = new STLTapReportEntities();
            model.styles = context.styles.ToList();
            return View(model);
        }

        public ActionResult Scoreboard()
        {
            STLTapReportEntities context = new STLTapReportEntities();
            ScoreboardModel scores = new ScoreboardModel();

            // Generate list of beers and styles, sorted by number of associated users 
            List<beer> SortedBeers = context.beers.OrderBy(x => x.users.Count()).ToList();
            List<style> SortedStyles = context.styles.OrderBy(x => x.users.Count()).ToList();

            // Populate scores with names and counts for first, second, and third place.
            scores.FirstBeer = SortedBeers.Last().name;
            scores.FirstBeerPic = SortedBeers.Last().imageurl;
            scores.FirstBCount = SortedBeers.Last().users.Count();
            scores.SecondBeer = SortedBeers.ElementAt(SortedBeers.Count() - 2).name;
            scores.SecondBCount = SortedBeers.ElementAt(SortedBeers.Count() - 2).users.Count();
            scores.ThirdBeer = SortedBeers.ElementAt(SortedBeers.Count() - 3).name;
            scores.ThirdBCount = SortedBeers.ElementAt(SortedBeers.Count() - 3).users.Count();

            scores.FirstStyle = SortedStyles.Last().name;
            if (SortedStyles.Last().beers.Count() > 0) //check to see if example of style exists before setting image
                scores.FirstStylePic = SortedStyles.Last().beers.First().imageurl;
            scores.FirstSCount = SortedStyles.Last().users.Count();
            scores.SecondStyle = SortedStyles.ElementAt(SortedStyles.Count() - 2).name;
            scores.SecondSCount = SortedStyles.ElementAt(SortedStyles.Count() - 2).users.Count();
            scores.ThirdStyle = SortedStyles.ElementAt(SortedStyles.Count() - 3).name;
            scores.ThirdSCount = SortedStyles.ElementAt(SortedStyles.Count() - 3).users.Count();

            return View(scores);
        }

        //
        // Individual beer profile and adding to list of favorites
        //

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
            
            // limit user to 3 beers
            if (thisuser.beers.Count() < 3)
            {
                //Add the beer from the profile to the user's beers list (populates junction table)
                context.users.Attach(thisuser);
                var user = context.Entry(thisuser);
                user.Collection(x => x.beers).CurrentValue.Add(beermodel);

                //Make sure database knows to update and save changes
                context.Entry(thisuser).State = System.Data.Entity.EntityState.Unchanged;
                context.SaveChanges();
            }
            return RedirectToAction("Beers");
        }
        
        //
        // Individual style profile and adding to list of favorites
        //

        public ActionResult StyleProfile(int StyleProfileId)
        {
            STLTapReportEntities context = new STLTapReportEntities();
            style model = new style();
            model = context.styles.Where(x => x.styleID == StyleProfileId).SingleOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult StyleProfile(style model)
        {
            
            STLTapReportEntities context = new STLTapReportEntities();
            
            //Get style selected from style profile page, get user
            style stylemodel = context.styles.Where(x => x.styleID == model.styleID).SingleOrDefault();
            int thisuserID = (int)Session["UserID"];
            user thisuser = context.users.Where(x => x.userID == thisuserID).SingleOrDefault();

            // limit user to 3 styles
            if (thisuser.styles.Count() < 3)
            {
                //Add the style from the profile to the user's styles list (populates junction table)
                context.users.Attach(thisuser);
                var user = context.Entry(thisuser);
                user.Collection(x => x.styles).CurrentValue.Add(stylemodel);

                //Make sure database knows to update and save changes
                context.Entry(thisuser).State = System.Data.Entity.EntityState.Unchanged;
                context.SaveChanges();
            }

            return RedirectToAction("Styles");
        }

        // Display a randomly selected beer as a partial view on the homepage
        [ChildActionOnly]
        public ActionResult _FeatureBeer(beer model)
        {
            STLTapReportEntities context = new STLTapReportEntities();
            Random rand = new Random();
            int RandomBeerIndex = rand.Next(context.beers.Count() - 1);
            var BeerList = context.beers.ToList();
            model = BeerList.ElementAt(RandomBeerIndex);
            return PartialView(model);
        }

    }
}
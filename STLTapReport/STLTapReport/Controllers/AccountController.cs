using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STLTapReport.Models;
using STLTapReport.data;

namespace STLTapReport.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(user model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                STLTapReportEntities context = new STLTapReportEntities();
                model.password = model.password.GetHashCode().ToString();
                context.users.Add(model);
                context.SaveChanges();

                //Log user in after creating account
                Session["logged_in"] = true;
                Session["name"] = model.name;
                Session["UserID"] = model.userID;
                return View("UserPage", model);
            }
        }

        public ActionResult Login()
        {
            //If logged in, logout
            if (!(Session["logged_in"] == null) && ((bool)Session["logged_in"]))
            {
                Session.Abandon();
                return RedirectToAction("Welcome", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
 
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                STLTapReportEntities context = new STLTapReportEntities();
                string hashedPassword = model.Password.GetHashCode().ToString();
                user user = context.users.Where(u => u.name == model.UserName && u.password == hashedPassword).SingleOrDefault();
                if (user == null)
                {
                    return View(model);
                }
                Session["logged_in"] = true;
                Session["name"] = model.UserName;
                Session["UserID"] = user.userID;

                if (user.isAdmin)
                {
                    Session["is_Admin"] = true;
                    return RedirectToAction("AdminWelcome", "Admin");
                }
                else
                {
                    return RedirectToAction("Welcome", "Home");
                }
            }
        }
        public ActionResult UserPage(user model)
        {
            STLTapReportEntities context = new STLTapReportEntities();
            string name = (string)Session["name"];
            model = context.users.Where(u => u.name == name).SingleOrDefault();
            return View(model);
        }

        public ActionResult RemoveUserItem(int itemID, string itemType)
        {
            STLTapReportEntities context = new STLTapReportEntities();
            string name = (string)Session["name"];
            user thisuser = context.users.Where(u => u.name == name).SingleOrDefault();

            if (itemType == "style")   // if item to remove is a style
            {
                // Get selected style from styles in user's list, remove, and save changes
                context.users.Attach(thisuser);
                var user = context.Entry(thisuser);
                style thisstyle = thisuser.styles.Where(x => x.styleID == itemID).SingleOrDefault();
                user.Collection(m => m.styles).CurrentValue.Remove(thisstyle);
                context.Entry(thisuser).State = System.Data.Entity.EntityState.Unchanged;
                context.SaveChanges();
                return RedirectToAction("UserPage", "Account");
            }
            else if (itemType == "beer")    // if item to remove is a beer
            {
                // Get selected beer from beers in user's list, remove, and save changes
                context.users.Attach(thisuser);
                var user = context.Entry(thisuser);
                beer thisbeer = thisuser.beers.Where(x => x.beerID == itemID).SingleOrDefault();
                user.Collection(m => m.beers).CurrentValue.Remove(thisbeer);
                context.Entry(thisuser).State = System.Data.Entity.EntityState.Unchanged;
                context.SaveChanges();
                return RedirectToAction("UserPage", "Account");
            }
            else       // if error (i.e., involving query string)
            {
                return RedirectToAction("Welcome", "Home");
            }

        }

    }
}

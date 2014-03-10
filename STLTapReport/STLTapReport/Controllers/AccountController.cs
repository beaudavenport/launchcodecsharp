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
        public ActionResult SignUp(SignUpModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else if (model.HumanVal != "Yes, I'm human") // custom validation
            {
                ModelState.AddModelError("", "You failed the human test. Please try again.");
                return View(model);
            }
            else
            {
                STLTapReportEntities context = new STLTapReportEntities();

                //hash password before proceeding
                model.password = model.password.GetHashCode().ToString();

                //check for duplicate users
                user DuplicateUser = context.users.Where(u => u.name == model.name).SingleOrDefault();
                if (DuplicateUser != null)
                {
                    ModelState.AddModelError("", "That user name already exists. Please choose another.");
                    return View(model);
                }

                //fill in values for new user and add to database
                user NewUser = new user();
                NewUser.name = model.name;
                NewUser.password = model.password;
                context.users.Add(NewUser);
                context.SaveChanges();

                //Log user in after creating account
                Session["logged_in"] = true;
                Session["name"] = NewUser.name;
                Session["UserID"] = NewUser.userID;
                return View("UserPage", NewUser);
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
        public ActionResult Login(user model)
        {
 
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                STLTapReportEntities context = new STLTapReportEntities();
                string hashedPassword = model.password.GetHashCode().ToString();
                user user = context.users.Where(u => u.name == model.name && u.password == hashedPassword).SingleOrDefault();
                if (user == null)
                {
                    ModelState.AddModelError("", "Your username or password are incorrect. Please try again.");
                    return View(model);
                }
                Session["logged_in"] = true;
                Session["name"] = model.name;
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

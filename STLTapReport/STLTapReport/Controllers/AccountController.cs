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

    }
}

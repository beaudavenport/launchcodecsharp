using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STLTapReport.Models;

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
            if (ModelState.IsValid)
            {
                return RedirectToAction("Beers", "Home");
            }
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            bool isValid = IsValidUser(model);
            if (!isValid)
            {
                ModelState.AddModelError("", "Sorry, your user name or password are invalid.");
                return View(model);
            }
            else
            {
                Session["logged_in"] = true;
                Session["name"] = model.UserName;
                return RedirectToAction("Beers", "Home");
            }
        }

        //Check for valid user
        private bool IsValidUser(LoginModel model)
        {
            if (model.UserName == "beau" && model.Password == "password")
            {
                return true;
            }
            return false;
        }
    }
}

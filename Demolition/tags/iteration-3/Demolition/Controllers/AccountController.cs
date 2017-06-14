using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Demolition.Models;

namespace Demolition.Controllers
{

    public class AccountController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewData["PasswordLength"] = 1;

            base.OnActionExecuting(filterContext);
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            /*
            if (ModelState.IsValid)
            {
                Models.User userModel = new Models.User();

                if (userModel.ValidateUser(Request.Cookies["username"].Value, model.OldPassword))
                {
                    
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }*/

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        public ActionResult LogOff()
        {
            Response.Cookies["username"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["password"].Expires = DateTime.Now.AddDays(-1);

            return RedirectToAction("LogOn", "Account");
        }

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings",
            Justification = "Needs to take same parameter type as Controller.Redirect()")]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User userModel = Models.User.Find(model.UserName, model.Password);

                if (userModel != null)
                {
                    HttpCookie UNHC = new HttpCookie("username", model.UserName);
                    Response.Cookies.Add(UNHC);
                    HttpCookie PWHC = new HttpCookie("password", model.Password);
                    Response.Cookies.Add(PWHC);

                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Demos");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                Models.User user = Models.User.Create(model.UserName, model.Password, model.Email, Models.User.Roles.Salesperson);

                if (user != null)
                {

                    HttpCookie UNHC = new HttpCookie("username", model.UserName);
                    Request.Cookies.Add(UNHC);
                    HttpCookie PWHC = new HttpCookie("password", model.Password);
                    Request.Cookies.Add(PWHC);  

                    return RedirectToAction("Index", "Demos");
                }
                else
                {
                    ModelState.AddModelError("", "");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

    }
}

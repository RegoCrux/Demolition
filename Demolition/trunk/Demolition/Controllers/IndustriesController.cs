using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demolition.Models;
using System.Web.Script.Serialization;

namespace Demolition.Controllers
{
    [AuthFilter]
    public class IndustriesController : Controller
    {
        public ActionResult Index()
        {
            return View(Industry.ListAll());
        }

        public ActionResult Details(int id)
        {
            if (!User.IsInRole(Properties.Settings.Default.AdministratorRole))
                return Redirect("/");

            return View(Industry.Find(id));
        }

        public ActionResult Create()
        {
            if (!User.IsInRole(Properties.Settings.Default.AdministratorRole))
                return Redirect("/");

            return View(new Industry());
        }

        [HttpPost]
        public ActionResult Create(Industry industry)
        {
            if (!User.IsInRole(Properties.Settings.Default.AdministratorRole))
                return Redirect("/");

            if (ModelState.IsValid)
            {
                industry.Save();
                return RedirectToAction("Details", new { id = industry.Id });
            }
            return View();

        }

        public ActionResult Edit(int id)
        {
            return View();
        }


        [HttpPost]
        public ActionResult Edit(int id, FormCollection form)
        {
            var industry = Industry.Find(id);
            industry.Update(form[0]);
            return Content("Updated Data.");
        }
    }
}

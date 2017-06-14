using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demolition.Models;
using System.IO;

namespace Demolition.Controllers
{
    [AuthFilter(Order = 0)]
    [AdminFilter(Order = 1)]
    public class AppsController : Controller
    {
        public ActionResult Index()
        {
            return View(App.ListAll());
        }

        public ActionResult Details(int id)
        {
            return View(App.Find(id));
        }

        public ActionResult Create()
        {
            return View(new App());
        }

        [HttpPost]
        public ActionResult Create(App app)
        {
            if (ModelState.IsValid)
            {
                app.Save();
                return RedirectToAction("Details", new { id = app.Id });
            }
					return View();
        }

        public ActionResult Edit(int id)
        {
            return View(App.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, App app)
        {
            if (ModelState.IsValid)
            {
                app.Save();
                return RedirectToAction("Details", new { id = app.Id });
            }
					return View();
        }
    }
}

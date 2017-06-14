using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demolition.Models;
using System.IO;

namespace Demolition.Controllers
{
    [AuthFilter]
    public class AppsController : Controller
    {
        // GET: /Apps
        public ActionResult Index()
        {
            return View(App.ListAll());
        }

        // GET: /App/Details/5
        public ActionResult Details(int id)
        {
            if (!User.IsInRole("Administrator"))
                return Redirect("/");
            else
                return View(App.Find(id));
        }

        // GET: /App/Create
        public ActionResult Create()
        {
            if (!User.IsInRole("Administrator"))
                return Redirect("/");
            else
                return View(new App());
        } 

        // POST: /App/Create
        [HttpPost]
        public ActionResult Create(App app)
        {
            if (!User.IsInRole("Administrator"))
                return Redirect("/");

            if (ModelState.IsValid)
            {
                app.Save();
                return RedirectToAction("Details", new { id = app.Id });
            }
            else
            {
                return View();
            }
        }

        // GET: /App/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: /App/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

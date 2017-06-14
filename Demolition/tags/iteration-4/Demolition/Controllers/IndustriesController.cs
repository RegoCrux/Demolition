﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Demolition.Models;

namespace Demolition.Controllers
{
    public class IndustriesController : Controller
    {
        //
        // GET: /Demos/

        public ActionResult Index()
        {
            return View(Industry.ListAll());
        }

        //
        // GET: /Industries/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Industries/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Industries/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Industries/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Industries/Edit/5

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

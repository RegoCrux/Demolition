using System.Web.Mvc;
using Demolition.Models;

namespace Demolition.Controllers
{
    [AuthFilter]
    public class DemosController : Controller
    {
        public ActionResult Index()
        {
            if (User.IsInRole("Administrator"))
            {
                return View(Demo.ListAll());
            }
            else
            {
                return View(Demo.ListByUser(User as Models.User));
            }
        }

        public ActionResult Details(int id)
        {
            var demo = Demo.Find(id);

            if (User.IsInRole("Administrator") || demo.User.Id == (User as Models.User).Id)
            {
                return View(demo);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Create()
        {
            return View(new Demo());
        } 

        [HttpPost]
        public ActionResult Create(Demo demo)
        {
            if (ModelState.IsValid)
            {
                demo.Save(User as Models.User);
                return RedirectToAction("Index");
            }
            else
            {
                return View(demo);
            }
        }

        public ActionResult SingleSignOn(int id)
        {
            return View(Demo.Find(id));
        }

        public ActionResult Showcase(int id)
        {
            ViewData["HideTop"] = true;
            return View(Demo.Find(id));
        }

        [AcceptVerbs(HttpVerbs.Delete)]
        public ActionResult Destroy(int id)
        {
            var demo = Demo.Find(id);
            demo.Destroy();
            return Content("Shutting down demo");
        }
    }
}

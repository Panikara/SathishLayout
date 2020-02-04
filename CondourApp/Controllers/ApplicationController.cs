using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CondourApp.Models.Edmx;

namespace CondourApp.Controllers
{
    public class ApplicationController : Controller
    {
        ConsultancyEntities db = new ConsultancyEntities();
        // GET: Application
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Application application)
        {
            db.Applications.Add(application);
            db.SaveChanges();
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TTIPApplication.Models;

namespace TTIPApplication.Controllers
{
    public class HomeController : Controller
    {
        private TTIP_DBEntities db = new TTIP_DBEntities();

        public ActionResult Index()
        {
            var Home = db.REVIEW.Include(p => p.PLACE).OrderByDescending(r => r.REVIEW_ID);

            return View(Home);
        }

        public ActionResult About()
        {
            ViewBag.Message = "TEST";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
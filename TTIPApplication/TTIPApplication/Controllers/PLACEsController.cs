using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TTIPApplication.Models;

namespace TTIPApplication.Controllers
{
    public class PLACEsController : Controller
    {
        private TTIP_DB_1Entities db = new TTIP_DB_1Entities();

        // GET: PLACEs
        public ActionResult Index()
        {
            var pLACE = db.PLACE.Include(p => p.category1).Include(p => p.CITY1);
            return View(pLACE.ToList());
        }

        // GET: PLACEs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PLACE pLACE = db.PLACE.Find(id);
            if (pLACE == null)
            {
                return HttpNotFound();
            }
            return View(pLACE);
        }

        // GET: PLACEs/Create
        public ActionResult Create()
        {
            ViewBag.category = new SelectList(db.category, "category_name", "category_name");
            ViewBag.CITY = new SelectList(db.CITY, "CITYNAME", "CITYNAME");
            return View();
        }

        // POST: PLACEs/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,STORE_NAME,CITY,category,place_ex")] PLACE pLACE)
        {
            if (ModelState.IsValid)
            {
                db.PLACE.Add(pLACE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.category = new SelectList(db.category, "category_name", "category_name", pLACE.category);
            ViewBag.CITY = new SelectList(db.CITY, "CITYNAME", "CITYNAME", pLACE.CITY);
            return View(pLACE);
        }

        // GET: PLACEs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PLACE pLACE = db.PLACE.Find(id);
            if (pLACE == null)
            {
                return HttpNotFound();
            }
            ViewBag.category = new SelectList(db.category, "category_name", "category_name", pLACE.category);
            ViewBag.CITY = new SelectList(db.CITY, "CITYNAME", "CITYNAME", pLACE.CITY);
            return View(pLACE);
        }

        // POST: PLACEs/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,STORE_NAME,CITY,category,place_ex")] PLACE pLACE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pLACE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category = new SelectList(db.category, "category_name", "category_name", pLACE.category);
            ViewBag.CITY = new SelectList(db.CITY, "CITYNAME", "CITYNAME", pLACE.CITY);
            return View(pLACE);
        }

        // GET: PLACEs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PLACE pLACE = db.PLACE.Find(id);
            if (pLACE == null)
            {
                return HttpNotFound();
            }
            return View(pLACE);
        }

        // POST: PLACEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PLACE pLACE = db.PLACE.Find(id);
            db.PLACE.Remove(pLACE);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

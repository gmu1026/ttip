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
    public class CityController : Controller
    {
        private TTIP_DBEntities db = new TTIP_DBEntities();

        // GET: City
        public ActionResult Index()
        {
            return View(db.CITY.ToList());
        }

        // GET: City/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CITY cITY = db.CITY.Find(id);
            if (cITY == null)
            {
                return HttpNotFound();
            }
            return View(cITY);
        }

        // GET: City/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: City/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CITY_NAME")] CITY cITY)
        {
            if (ModelState.IsValid)
            {
                db.CITY.Add(cITY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cITY);
        }

        // GET: City/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CITY cITY = db.CITY.Find(id);
            if (cITY == null)
            {
                return HttpNotFound();
            }
            return View(cITY);
        }

        // POST: City/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CITY_NAME")] CITY cITY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cITY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cITY);
        }

        // GET: City/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CITY cITY = db.CITY.Find(id);
            if (cITY == null)
            {
                return HttpNotFound();
            }
            return View(cITY);
        }

        // POST: City/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CITY cITY = db.CITY.Find(id);
            db.CITY.Remove(cITY);
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

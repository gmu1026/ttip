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
    public class CITiesController : Controller
    {
        private ttipEntities1 db = new ttipEntities1();

        // GET: CITies
        public ActionResult Index()
        {
            return View(db.CITY.ToList());
        }

        // GET: CITies/Details/5
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

        // GET: CITies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CITies/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CITYNAME")] CITY cITY)
        {
            if (ModelState.IsValid)
            {
                db.CITY.Add(cITY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cITY);
        }

        // GET: CITies/Edit/5
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

        // POST: CITies/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CITYNAME")] CITY cITY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cITY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cITY);
        }

        // GET: CITies/Delete/5
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

        // POST: CITies/Delete/5
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

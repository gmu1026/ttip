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
    public class ReviewController : Controller
    {
        private TTIP_DBEntities db = new TTIP_DBEntities();

        // GET: Review
        public ActionResult Index()
        {
            var rEVIEW = db.REVIEW.Include(r => r.PLACE);
            return View(rEVIEW.ToList());
        }

        // GET: Review/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REVIEW rEVIEW = db.REVIEW.Find(id);
            if (rEVIEW == null)
            {
                return HttpNotFound();
            }
            return View(rEVIEW);
        }

        // GET: Review/Create
        public ActionResult Create()
        {
            ViewBag.PID = new SelectList(db.PLACE, "ID", "STORE_NAME");
            return View();
        }

        // POST: Review/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "REVIEW_ID,PID,WRITER,SCORE,UPDATE_DATE,REVIEW_COMMENT")] REVIEW rEVIEW)
        {
            if (ModelState.IsValid)
            {
                db.REVIEW.Add(rEVIEW);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PID = new SelectList(db.PLACE, "ID", "STORE_NAME", rEVIEW.PID);
            return View(rEVIEW);
        }

        // GET: Review/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REVIEW rEVIEW = db.REVIEW.Find(id);
            if (rEVIEW == null)
            {
                return HttpNotFound();
            }
            ViewBag.PID = new SelectList(db.PLACE, "ID", "STORE_NAME", rEVIEW.PID);
            return View(rEVIEW);
        }

        // POST: Review/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "REVIEW_ID,PID,WRITER,SCORE,UPDATE_DATE,REVIEW_COMMENT")] REVIEW rEVIEW)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rEVIEW).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PID = new SelectList(db.PLACE, "ID", "STORE_NAME", rEVIEW.PID);
            return View(rEVIEW);
        }

        // GET: Review/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            REVIEW rEVIEW = db.REVIEW.Find(id);
            if (rEVIEW == null)
            {
                return HttpNotFound();
            }
            return View(rEVIEW);
        }

        // POST: Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            REVIEW rEVIEW = db.REVIEW.Find(id);
            db.REVIEW.Remove(rEVIEW);
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

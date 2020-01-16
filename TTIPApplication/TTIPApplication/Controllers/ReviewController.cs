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
        private TTIP_DB_1Entities db = new TTIP_DB_1Entities();

        // GET: Review
        public ActionResult Index()
        {
            var review_ = db.review_.Include(r => r.PLACE);
            return View(review_.ToList());
        }

        // GET: Review/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            review_ review_ = db.review_.Find(id);
            if (review_ == null)
            {
                return HttpNotFound();
            }
            return View(review_);
        }

        // GET: Review/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.PLACE, "ID", "STORE_NAME");
            return View();
        }

        // POST: Review/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "REVIEW_NUM,ID,WRITER,SCORE,UPDATE_DATE,substance")] review_ review_)
        {
            if (ModelState.IsValid)
            {
                db.review_.Add(review_);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.PLACE, "ID", "STORE_NAME", review_.ID);
            return View(review_);
        }

        // GET: Review/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            review_ review_ = db.review_.Find(id);
            if (review_ == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.PLACE, "ID", "STORE_NAME", review_.ID);
            return View(review_);
        }

        // POST: Review/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "REVIEW_NUM,ID,WRITER,SCORE,UPDATE_DATE,substance")] review_ review_)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review_).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.PLACE, "ID", "STORE_NAME", review_.ID);
            return View(review_);
        }

        // GET: Review/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            review_ review_ = db.review_.Find(id);
            if (review_ == null)
            {
                return HttpNotFound();
            }
            return View(review_);
        }

        // POST: Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            review_ review_ = db.review_.Find(id);
            db.review_.Remove(review_);
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

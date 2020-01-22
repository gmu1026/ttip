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
    public class photosController : Controller
    {
        private ttipEntities1 db = new ttipEntities1();

        // GET: photos
        public ActionResult Index()
        {
            var photo = db.photo.Include(p => p.PLACE);
            return View(photo.ToList());
        }

        // GET: photos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            photo photo = db.photo.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // GET: photos/Create
        public ActionResult Create()
        {
            ViewBag.place_id = new SelectList(db.PLACE, "ID", "STORE_NAME");
            return View();
        }

        // POST: photos/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,photo_path,place_id")] photo photo)
        {
            if (ModelState.IsValid)
            {
                db.photo.Add(photo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.place_id = new SelectList(db.PLACE, "ID", "STORE_NAME", photo.place_id);
            return View(photo);
        }

        // GET: photos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            photo photo = db.photo.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            ViewBag.place_id = new SelectList(db.PLACE, "ID", "STORE_NAME", photo.place_id);
            return View(photo);
        }

        // POST: photos/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,photo_path,place_id")] photo photo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(photo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.place_id = new SelectList(db.PLACE, "ID", "STORE_NAME", photo.place_id);
            return View(photo);
        }

        // GET: photos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            photo photo = db.photo.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            photo photo = db.photo.Find(id);
            db.photo.Remove(photo);
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

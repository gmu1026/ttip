using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TTIPApplication.Models;

namespace TTIPApplication.Controllers
{
    public class PlaceController : Controller
    {
        private TTIP_DBEntities db = new TTIP_DBEntities();

        // GET: Place
        public ActionResult Index(string searchString)
        {
            var pLACE = db.PLACE.Include(p => p.CATEGORY1).Include(p => p.CITY1);
            ViewBag.CATEGORY = new SelectList(db.CATEGORY, "CATEGORY_NAME", "CATEGORY_NAME");
            ViewBag.CITY = new SelectList(db.CITY, "CITY_NAME", "CITY_NAME");
            
            if(!String.IsNullOrEmpty(searchString))
            {
                ViewBag.CATEGORY = new SelectList(db.CATEGORY, "CATEGORY_NAME", "CATEGORY_NAME");
                ViewBag.CITY = new SelectList(db.CITY, "CITY_NAME", "CITY_NAME");
                var store = from s in db.PLACE select s;
                store = store.Where(s => s.STORE_NAME.Contains(searchString));
                return View(store);
            }
            return View(pLACE);
        }

        // GET: Place/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PLACE pLACE = db.PLACE.Find(id);
            ViewBag.reviews = db.REVIEW.Where(r => r.PID == id).ToList();
            if (pLACE == null)
            {
                return HttpNotFound();
            }
            return View(pLACE);
        }

        // GET: Place/Create
        public ActionResult Create()
        {
            ViewBag.CATEGORY = new SelectList(db.CATEGORY, "CATEGORY_NAME", "CATEGORY_NAME");
            ViewBag.CITY = new SelectList(db.CITY, "CITY_NAME", "CITY_NAME");
            return View();
        }

        // POST: Place/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,STORE_NAME,CITY,CATEGORY,EXPLANATION,ADDRESS,DETAIL_IMAGE")] PLACE pLACE)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null 
                        && file.ContentLength > 0)
                    {
                        var fileName = "place_img_" + pLACE.ID + "_" + Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/images/"), fileName);
                        file.SaveAs(path);
                        pLACE.DETAIL_IMAGE = fileName;
                    }
                }
                db.PLACE.Add(pLACE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CATEGORY = new SelectList(db.CATEGORY, "CATEGORY_NAME", "CATEGORY_NAME", pLACE.CATEGORY);
            ViewBag.CITY = new SelectList(db.CITY, "CITY_NAME", "CITY_NAME", pLACE.CITY);
            return View(pLACE);
        }

        // GET: Place/Edit/5
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
            ViewBag.CATEGORY = new SelectList(db.CATEGORY, "CATEGORY_NAME", "CATEGORY_NAME", pLACE.CATEGORY);
            ViewBag.CITY = new SelectList(db.CITY, "CITY_NAME", "CITY_NAME", pLACE.CITY);
            return View(pLACE);
        }

        // POST: Place/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,STORE_NAME,CITY,CATEGORY,EXPLANATION,ADDRESS,DETAIL_IMAGE")] PLACE pLACE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pLACE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CATEGORY = new SelectList(db.CATEGORY, "CATEGORY_NAME", "CATEGORY_NAME", pLACE.CATEGORY);
            ViewBag.CITY = new SelectList(db.CITY, "CITY_NAME", "CITY_NAME", pLACE.CITY);
            return View(pLACE);
        }

        // GET: Place/Delete/5
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

        // POST: Place/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PLACE pLACE = db.PLACE.Find(id);
            if (pLACE.DETAIL_IMAGE != null)
            {
                var image_path = Path.Combine(Server.MapPath("~/images/"), pLACE.DETAIL_IMAGE);
                System.IO.File.Delete(image_path);
            }

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

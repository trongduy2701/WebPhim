using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsitePhimHoatHinh18.Models.EF;
using WebsitePhimHoatHinh18.Areas.Admin.Models;
using PagedList;

namespace WebsitePhimHoatHinh18.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class GenresController : Controller
    {
        private WebsitePhimHoatHinh18DbContext db = new WebsitePhimHoatHinh18DbContext();

        public ActionResult Index(int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var genres = db.Genres.ToList();
            return View(genres.ToPagedList(pageNumber, pageSize));
        }

        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Genres genres = db.Genres.Find(id);
        //    if (genres == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(genres);
        //}

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GenreID,GenreName")] Genres genres)
        {
            if (ModelState.IsValid)
            {
                db.Genres.Add(genres);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(genres);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genres genres = db.Genres.Find(id);
            if (genres == null)
            {
                return HttpNotFound();
            }
            return View(genres);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GenreID,GenreName")] Genres genres)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genres).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genres);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genres genres = db.Genres.Find(id);
            if (genres == null)
            {
                return HttpNotFound();
            }
            return View(genres);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Genres genres = db.Genres.Find(id);
            db.Genres.Remove(genres);
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

using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsitePhimHoatHinh18.Areas.Admin.Models;
using WebsitePhimHoatHinh18.Models.EF;

namespace WebsitePhimHoatHinh18.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class FavoritesController : Controller
    {
        private WebsitePhimHoatHinh18DbContext db = new WebsitePhimHoatHinh18DbContext();

        public ActionResult Index(int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            var favorites = db.Favorites.Include(f => f.Movies).Include(f => f.Users);
            return View(favorites.ToList().ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorites favorites = db.Favorites.Find(id);
            if (favorites == null)
            {
                return HttpNotFound();
            }
            return View(favorites);
        }

        //public ActionResult Create()
        //{
        //    ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "Tilte");
        //    ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "FavariteID,UserID,MovieID,CreatedDate")] Favorites favorites)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Favorites.Add(favorites);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "Tilte", favorites.MovieID);
        //    ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", favorites.UserID);
        //    return View(favorites);
        //}

        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Favorites favorites = db.Favorites.Find(id);
        //    if (favorites == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "Tilte", favorites.MovieID);
        //    ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", favorites.UserID);
        //    return View(favorites);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "FavariteID,UserID,MovieID,CreatedDate")] Favorites favorites)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(favorites).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "Tilte", favorites.MovieID);
        //    ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", favorites.UserID);
        //    return View(favorites);
        //}

        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Favorites favorites = db.Favorites.Find(id);
        //    if (favorites == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(favorites);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Favorites favorites = db.Favorites.Find(id);
        //    db.Favorites.Remove(favorites);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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

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
    public class ReportsController : Controller
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
            var reports = db.Reports.Include(r => r.Movies).Include(r => r.Users);
            return View(reports.ToList().ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reports reports = db.Reports.Find(id);
            if (reports == null)
            {
                return HttpNotFound();
            }
            return View(reports);
        }

        ////public ActionResult Create()
        ////{
        ////    ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "Tilte");
        ////    ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
        ////    return View();
        ////}

        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public ActionResult Create([Bind(Include = "ReportID,UserID,MovieID,ReportText,CreatedDate")] Reports reports)
        ////{
        ////    if (ModelState.IsValid)
        ////    {
        ////        db.Reports.Add(reports);
        ////        db.SaveChanges();
        ////        return RedirectToAction("Index");
        ////    }

        ////    ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "Tilte", reports.MovieID);
        ////    ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", reports.UserID);
        ////    return View(reports);
        ////}

        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Reports reports = db.Reports.Find(id);
        //    if (reports == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "Tilte", reports.MovieID);
        //    ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", reports.UserID);
        //    return View(reports);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ReportID,UserID,MovieID,ReportText,CreatedDate")] Reports reports)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(reports).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "Tilte", reports.MovieID);
        //    ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", reports.UserID);
        //    return View(reports);
 
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reports reports = db.Reports.Find(id);
            if (reports == null)
            {
                return HttpNotFound();
            }
            return View(reports);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reports reports = db.Reports.Find(id);
            db.Reports.Remove(reports);
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

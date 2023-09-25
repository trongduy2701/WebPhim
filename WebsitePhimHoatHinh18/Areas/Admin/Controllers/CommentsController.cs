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
    public class CommentsController : Controller
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
            var comments = db.Comments.Include(c => c.Movies).Include(c => c.Users);
            return View(comments.ToList().ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Comments.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            return View(comments);
        }

        //public ActionResult Create()
        //{
        //    ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "Tilte");
        //    ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
        //    return View();
        //}

        //// POST: Admin/Comments/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "CommentID,UserID,MovieID,CommentText,Rating,CreatedDate")] Comments comments)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Comments.Add(comments);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "Tilte", comments.MovieID);
        //    ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", comments.UserID);
        //    return View(comments);
        //}

        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Comments comments = db.Comments.Find(id);
        //    if (comments == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "Tilte", comments.MovieID);
        //    ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", comments.UserID);
        //    return View(comments);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "CommentID,UserID,MovieID,CommentText,Rating,CreatedDate")] Comments comments)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(comments).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.MovieID = new SelectList(db.Movies, "MovieID", "Tilte", comments.MovieID);
        //    ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", comments.UserID);
        //    return View(comments);
        //}

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Comments.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            return View(comments);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comments comments = db.Comments.Find(id);
            db.Comments.Remove(comments);
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

using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsitePhimHoatHinh18.Models.EF;

namespace WebsitePhimHoatHinh18.Controllers
{
    public class CommentUController : Controller
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

            if (Session["User"] != null)
            {
                Users user = (Users)Session["User"];
                int userID = Convert.ToInt32(user.UserID);
                var commentsList = db.Comments
                    .Where(f => f.UserID == userID)
                    .ToList();
                return View(commentsList.ToPagedList(pageNumber, pageSize));
            }

            return RedirectToAction("Login", "Home", null);
        }

        [HttpPost]
        public ActionResult Create(Comments comments, int? id, string text, int rate)
        {
            if (Session["User"] != null)
            {
                if (ModelState.IsValid)
                {
                    Users user = (Users)Session["User"];
                    int userID = Convert.ToInt32(user.UserID);

                    Movies movies = db.Movies.Find(id);
                    movies = db.Movies.FirstOrDefault(m => m.MovieID == id);

                    if (!string.IsNullOrEmpty(text) && rate >= 1 && rate <= 5)
                    {
                        comments.UserID = userID;
                        comments.MovieID = movies.MovieID;
                        comments.CommentText = text;
                        comments.Rating = rate;
                        comments.CreatedDate = DateTime.Now;

                        db.Comments.Add(comments);
                        db.SaveChanges();
                        return RedirectToAction("Details", "MovieU", new { id = id });
                    }
                }

            }
            return RedirectToAction("Details", "MovieU", new { id = id });
        }
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
        public ActionResult DeleteConfirmed(int? id)
        {
            Comments comments = db.Comments.Find(id);
            db.Comments.Remove(comments);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

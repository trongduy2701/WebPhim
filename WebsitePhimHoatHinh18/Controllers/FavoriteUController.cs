using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsitePhimHoatHinh18.Models.EF;

namespace WebsitePhimHoatHinh18.Controllers
{
    public class FavoriteUController : Controller
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
                var favoritesList = db.Favorites
                    .Where(f => f.UserID == userID)
                    .ToList();
                return View(favoritesList.ToPagedList(pageNumber, pageSize));
            }

            return RedirectToAction("Login", "Home", null);
        }

        public ActionResult Create(Favorites favorites, int? id)
        {
            if (Session["User"] != null)
            {
                if (ModelState.IsValid)
                {
                    Users user = (Users)Session["User"];
                    int userID = Convert.ToInt32(user.UserID);

                    Movies movies = db.Movies.Find(id);
                    movies = db.Movies.FirstOrDefault(m => m.MovieID == id);

                    favorites.UserID = userID;
                    favorites.MovieID = movies.MovieID;
                    favorites.CreatedDate = DateTime.Now;

                    db.Favorites.Add(favorites);
                    db.SaveChanges();
                    return RedirectToAction("Details", "MovieU", new { id = id });
                }

            }
            return View(favorites);
        }

        public ActionResult Delete(int? id)
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Favorites favorites = db.Favorites.Find(id);
            db.Favorites.Remove(favorites);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

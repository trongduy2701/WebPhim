using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsitePhimHoatHinh18.Models.EF;
using PagedList;
using WebsitePhimHoatHinh18.Areas.Admin.Models;
using System.Collections.Generic;

namespace WebsitePhimHoatHinh18.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class MoviesController : Controller
    {
        private WebsitePhimHoatHinh18DbContext db = new WebsitePhimHoatHinh18DbContext();

        public ActionResult Index(int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var movies = db.Movies.Include(c => c.Genres).Include(c => c.Countries);
            return View(movies.ToList().ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movies movies = db.Movies.Find(id);
            if (movies == null)
            {
                return HttpNotFound();
            }

            var averageRates = db.Comments
                .Where(r => r.MovieID == id)
                .GroupBy(r => r.MovieID)
                .Select(g => new
                {
                    MoviesID = g.Key,
                    AverageRate = g.Average(r => r.Rating)
                });

            foreach (var averageRate in averageRates)
            {
                movies = db.Movies.FirstOrDefault(p => p.MovieID == averageRate.MoviesID);
                if (movies != null)
                {
                    movies.Rate = (int?)averageRate.AverageRate;
                }
            }

            db.SaveChanges();
            return View(movies);
        }

        public ActionResult Create()
        {
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName");
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "CountryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MovieID,GenreID,CountryID,Tilte,Director,Description,Year,View,Rate,Time,Image,Video")] Movies movies, HttpPostedFileBase imageFile, HttpPostedFileBase videoFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    string fileIName = Path.GetFileName(imageFile.FileName);
                    string fileIPath = Path.Combine(Server.MapPath("~/Uploads/Images/"), fileIName);
                    imageFile.SaveAs(fileIPath);
                    movies.Image = fileIName;

                }
                if (videoFile != null && videoFile.ContentLength > 0)
                {
                    string fileVName = Path.GetFileName(videoFile.FileName);
                    string fileVPath = Path.Combine(Server.MapPath("~/Uploads/Videos/"), fileVName);
                    videoFile.SaveAs(fileVPath);
                    movies.Video = fileVName;
                }
                db.Movies.Add(movies);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movies);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movies movies = db.Movies.Find(id);
            if (movies == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName");
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "CountryName");
            return View(movies);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MovieID,GenreID,CountryID,Tilte,Director,Description,Year,View,Rate,Time,Image,Video")] Movies movies, HttpPostedFileBase imageFile, HttpPostedFileBase videoFile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movies).State = EntityState.Detached;

                Movies existingmovie = db.Movies.Find(movies.MovieID);

                existingmovie.GenreID = movies.GenreID;
                existingmovie.CountryID = movies.CountryID;
                existingmovie.Tilte = movies.Tilte;
                existingmovie.Description = movies.Description;
                existingmovie.Director = movies.Director;
                existingmovie.Year = movies.Year;
                existingmovie.Time = movies.Time;
                existingmovie.View = movies.View;
                existingmovie.Rate = movies.Rate;

                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    if (!string.IsNullOrEmpty(existingmovie.Image))
                    {
                        string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/Images/"), existingmovie.Image);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    string fileIName = Path.GetFileName(imageFile.FileName);
                    string fileIPath = Path.Combine(Server.MapPath("~/Uploads/Images/"), fileIName);
                    imageFile.SaveAs(fileIPath);
                    existingmovie.Image = fileIName;
                }

                if (videoFile != null && videoFile.ContentLength > 0)
                {
                    if (!string.IsNullOrEmpty(existingmovie.Video))
                    {
                        string oldVideoPath = Path.Combine(Server.MapPath("~/Uploads/Videos/"), existingmovie.Video);
                        if (System.IO.File.Exists(oldVideoPath))
                        {
                            System.IO.File.Delete(oldVideoPath);
                        }
                    }
                    string fileVName = Path.GetFileName(videoFile.FileName);
                    string fileVPath = Path.Combine(Server.MapPath("~/Uploads/Videos/"), fileVName);
                    videoFile.SaveAs(fileVPath);
                    existingmovie.Video = fileVName;
                }

                db.Entry(existingmovie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName");
            ViewBag.CountryID = new SelectList(db.Countries, "CountryID", "CountryName");
            return View(movies);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movies movies = db.Movies.Find(id);
            if (movies == null)
            {
                return HttpNotFound();
            }
            return View(movies);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movies movies = db.Movies.Find(id);
            if (!string.IsNullOrEmpty(movies.Image))
            {
                string filePath = Path.Combine(Server.MapPath("~/Uploads/Images"), movies.Image);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            if (!string.IsNullOrEmpty(movies.Video))
            {
                string filePath = Path.Combine(Server.MapPath("~/Uploads/Videos"), movies.Video);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            db.Movies.Remove(movies);
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

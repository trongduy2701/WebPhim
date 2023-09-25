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
    public class MovieUController : Controller
    {
        private WebsitePhimHoatHinh18DbContext db = new WebsitePhimHoatHinh18DbContext();
        public ActionResult Index(int? page, string searchString)
        {
            if (page == null)
            {
                page = 1;
            }
            int pageSize = 12;
            int pageNumber = (page ?? 1);

            var movies = db.Movies.ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(m => m.Tilte.ToLower().Contains(searchString) ||
                    m.GenreID.HasValue && m.Genres.GenreName.ToLower().Contains(searchString) || 
                    m.CountryID.HasValue && m.Countries.CountryName.ToLower().Contains(searchString))
                    .ToList();
            }

            return View(movies.ToPagedList(pageNumber, pageSize));
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

            List<Comments> comments = movies.Comments.ToList();

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

            movies.View++;
            db.SaveChanges();
            return View(movies);
        }
    }
}

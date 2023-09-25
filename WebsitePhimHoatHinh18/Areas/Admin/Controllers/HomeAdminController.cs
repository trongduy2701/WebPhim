using WebsitePhimHoatHinh18.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsitePhimHoatHinh18.Areas.Admin.Models;
using System.Data.Entity;

namespace WebsitePhimHoatHinh18.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        private WebsitePhimHoatHinh18DbContext db = new WebsitePhimHoatHinh18DbContext();

        public ActionResult Index()
        {
            if (Session["Role"] != null && Session["Role"].ToString() == "Admin")
            {
                var topViewMovies = db.Movies.OrderByDescending(m => m.View).Take(4).ToList();
                var topRateMovies = db.Movies.OrderByDescending(m => m.Rate).Take(4).ToList();
                var latestYearMovies = db.Movies.OrderByDescending(m => m.Year).Take(4).ToList();
                var topFavoriteMovies = db.Favorites
                                    .Where(f => f.MovieID != null)
                                    .GroupBy(f => f.MovieID)
                                    .OrderByDescending(g => g.Count())
                                    .Take(4)
                                    .Select(g => g.FirstOrDefault().Movies)
                                    .ToList();

                var viewModel = new MovieListViewModel
                {
                    TopViewMovies = topViewMovies,
                    TopRateMovies = topRateMovies,
                    LatestYearMovies = latestYearMovies,
                    TopFavoriteMovies = topFavoriteMovies
                };

                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Details(int? id)
        {
            var moviesInGroup = db.Movies.Where(m => m.GenreID == id || m.CountryID == id).ToList();
            return View(moviesInGroup);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Users user)
        {
            var userName = user.UserName;
            var password = user.Password;
            var checkUser = db.Users.FirstOrDefault(u => u.UserName == userName && u.Password == password);
            if (checkUser != null)
            {
                if (checkUser.Role != null)
                {
                    Session["Role"] = "Admin";
                    Session["User"] = checkUser;
                }
                else
                {
                    Session["Role"] = "User";
                    Session["User"] = checkUser;
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(user);
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
using WebsitePhimHoatHinh18.Models.EF;
using PagedList;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Data.Entity;
using System;
using WebsitePhimHoatHinh18.Areas.Admin.Models;

namespace WebsitePhimHoatHinh18.Controllers
{
    public class HomeController : Controller
    {
        private WebsitePhimHoatHinh18DbContext db = new WebsitePhimHoatHinh18DbContext();
        public ActionResult Index(Users user, string searchString)
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

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Users user)
        {
            var existingUser = db.Users.FirstOrDefault(u => u.UserName == user.UserName);
            var existingEmail = db.Users.FirstOrDefault(u => u.Email == user.Email);
            var existingPhone = db.Users.FirstOrDefault(u => u.Phone == user.Phone);

            if (existingUser != null)
            {
                ModelState.AddModelError("UserName", "Tên tài khoản đã tồn tại.");
                return View(user);
            }
            if (existingEmail != null)
            {
                ModelState.AddModelError("Email", "Email đã tồn tại.");
                return View(user);
            }
            if (existingPhone != null)
            {
                ModelState.AddModelError("Phone", "Số điện thoại đã tồn tại.");
                return View(user);
            }
            db.Users.Add(user);
            db.SaveChanges();
            TempData["SuccessMessage"] = "Đăng ký thành công. Vui lòng đăng nhập!";
            return RedirectToAction("Login");
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
                TempData["ErrorMessage"] = "Tên người dùng hoặc mật khẩu không hợp lệ. Vui lòng thử lại!";
                return View(user);
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
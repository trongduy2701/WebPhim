using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using WebsitePhimHoatHinh18.Models.EF;

namespace WebsitePhimHoatHinh18.Controllers
{
    public class ReportUController : Controller
    {
        private WebsitePhimHoatHinh18DbContext db = new WebsitePhimHoatHinh18DbContext();

        public ActionResult Create(Reports reports, int? id, string text)
        {
            if (Session["User"] != null)
            {
                if (ModelState.IsValid)
                {
                    Users user = (Users)Session["User"];
                    int userID = Convert.ToInt32(user.UserID);

                    Movies movies = db.Movies.Find(id);
                    movies = db.Movies.FirstOrDefault(m => m.MovieID == id);

                    reports.UserID = userID;
                    reports.MovieID = movies.MovieID;
                    reports.ReportText = text;
                    reports.CreatedDate = DateTime.Now;

                    db.Reports.Add(reports);
                    db.SaveChanges();
                    return RedirectToAction("Details", "MovieU", new { id = id });
                }

            }
            return View(reports);
        }
    }
}

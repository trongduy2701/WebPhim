using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsitePhimHoatHinh18.Models.EF;

namespace WebsitePhimHoatHinh18.Controllers
{
    public class UserUController : Controller
    {
        private WebsitePhimHoatHinh18DbContext db = new WebsitePhimHoatHinh18DbContext();
        public ActionResult Details(int? id)
        {
            if (Session["User"] != null)
            {
                Users user = (Users)Session["User"];
                int userID = Convert.ToInt32(user.UserID);
                id = Convert.ToInt32(userID);
                return View(user);
            }
            return RedirectToAction("Login", "Home", null);
        }

    }
}

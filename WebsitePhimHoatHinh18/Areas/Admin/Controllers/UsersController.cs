using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebsitePhimHoatHinh18.Models.EF;
using WebsitePhimHoatHinh18.Areas.Admin.Models;
using PagedList;

namespace WebsitePhimHoatHinh18.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class UsersController : Controller
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
            var users = db.Users.ToList();
            return View(users.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,RoleID,UserName,Password,Email,Phone")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(users);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,RoleID,UserName,Password,Email,Phone")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(users);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
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

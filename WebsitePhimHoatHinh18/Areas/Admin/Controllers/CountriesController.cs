using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsitePhimHoatHinh18.Models.EF;
using WebsitePhimHoatHinh18.Areas.Admin.Models;
using PagedList;

namespace WebsitePhimHoatHinh18.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class CountriesController : Controller
    {
        private WebsitePhimHoatHinh18DbContext db = new WebsitePhimHoatHinh18DbContext();

        public ActionResult Index(int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var countries = db.Countries.ToList();
            return View(countries.ToPagedList(pageNumber, pageSize));
        }

        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Countries countries = db.Countries.Find(id);
        //    if (countries == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(countries);
        //}

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CountryID,CountryName")] Countries countries)
        {
            if (ModelState.IsValid)
            {
                db.Countries.Add(countries);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(countries);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Countries countries = db.Countries.Find(id);
            if (countries == null)
            {
                return HttpNotFound();
            }
            return View(countries);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CountryID,CountryName")] Countries countries)
        {
            if (ModelState.IsValid)
            {
                db.Entry(countries).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(countries);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Countries countries = db.Countries.Find(id);
            if (countries == null)
            {
                return HttpNotFound();
            }
            return View(countries);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Countries countries = db.Countries.Find(id);
            db.Countries.Remove(countries);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestaurantReview.Core.Models;
using RestaurantReview.DB.DataContext;

namespace RestaurantReview.Web.Controllers {
    [Authorize]
    public class MenusController : Controller
    {
        private ReviewsDb db = new ReviewsDb();

        // GET: Menus
        [AllowAnonymous]
        public ActionResult Index()
        {
            var menues = db.Menues.Include(m => m.Restaurant).ToList();
            return View(menues);
        }
        
        //public ActionResult ViewMenu()
        //{
        //    var menues = db.Menues.Include(m => m.Restaurant);
        //    return View(menues.ToList());
        //}
        // GET: Menus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menues.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: Menus/Create
       
        public ActionResult Create()
        {
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name");
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Menues.Add(menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", menu.RestaurantId);
            return View(menu);
        }

        // GET: Menus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menues.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", menu.RestaurantId);
            return View(menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Food,Description,Price,RestaurantId")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", menu.RestaurantId);
            return View(menu);
        }

        // GET: Menus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menues.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = db.Menues.Find(id);
            db.Menues.Remove(menu);
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

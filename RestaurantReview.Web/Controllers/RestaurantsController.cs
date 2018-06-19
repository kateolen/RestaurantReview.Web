using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestaurantReview.Core.Models;
using RestaurantReview.DB.DataContext;

namespace RestaurantReview.Web.Controllers
{
    [Authorize]
    public class RestaurantsController : Controller
    {
        private ReviewsDb db = new ReviewsDb();

        // GET: Restaurants
        public ActionResult Index()
        {
            return View(db.Restaurants.ToList());
        }

        // GET: Restaurants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // GET: Restaurants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Restaurant restaurant, HttpPostedFileBase restaurantImage)
        {
            if (ModelState.IsValid)
            {
                if (restaurantImage != null)
                {
                    var baseUnc = Server.MapPath("~" + ConfigurationManager.AppSettings["RestaurantImageUploadBase"]);
                    //baseUrl/{rid}/{restaurantImage.FileName}
                    baseUnc = $"{baseUnc}\\{restaurant.Id}\\";

                    if (!Directory.Exists(baseUnc))
                    {
                        Directory.CreateDirectory(baseUnc);
                    }
                    restaurant.ImageUrl = restaurantImage.FileName;
                    restaurantImage.SaveAs(baseUnc + restaurantImage.FileName);
                }

                db.Restaurants.Add(restaurant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);

            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Restaurant restaurant, HttpPostedFileBase restaurantImage)
        {
            bool isValidDoc = false;
            string[] allowedExtentions = new string[] { ".jpg", ".png", ".gif", ".jpeg" };
            if (ModelState.IsValid)
            {

                if (restaurantImage != null && restaurantImage.ContentLength > 0)
                {
                    isValidDoc = allowedExtentions.Any(item => restaurantImage.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
                }
                if(isValidDoc)
                {
                    var baseUnc = Server.MapPath("~" + ConfigurationManager.AppSettings["RestaurantImageUploadBase"]);
                    //baseUrl/{rid}/{restaurantImage.FileName}
                    baseUnc = $"{baseUnc}\\{restaurant.Id}\\";

                    if (!Directory.Exists(baseUnc))
                    {
                        Directory.CreateDirectory(baseUnc);
                    }
                    restaurant.ImageUrl = restaurantImage.FileName;
                    restaurantImage.SaveAs(baseUnc + restaurantImage.FileName);
                }
                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            db.Restaurants.Remove(restaurant);
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

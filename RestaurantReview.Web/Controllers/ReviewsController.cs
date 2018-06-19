using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RestaurantReview.Core.Models;
using RestaurantReview.DB.DataContext;
using RestaurantReview.Web.Helpers;
using RestaurantReview.Web.Models;
using RestaurantReview.Web.Models.ViewModels;

namespace RestaurantReview.Web.Controllers
{
    public class ReviewsController : Controller
    {
        private ReviewsDb db = new ReviewsDb();

        // GET: Reviews
        public ActionResult Index(int id)
        {
            var rest = db.Restaurants.Include(r => r.Reviews).FirstOrDefault(p => p.Id == id);
            return View(rest);
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Include(p=>p.Restaurant).FirstOrDefault(p=>p.Id == id);
            ReviewVm reviewVm = new ReviewVm();
            reviewVm.Rating = review.Rating;
            reviewVm.Body = review.Body;
            reviewVm.ReviewerName = review.ReviewerName;
            reviewVm.RestaurantName = review.Restaurant.Name;

            if (review == null)
            {
                return HttpNotFound();
            }
            return View(reviewVm);
        }

        // GET: Reviews/Create
        public ActionResult Manage(int? id, int? restaurantId)
        {
            var review = new ReviewVm();
            if (restaurantId.HasValue)
            {
                var r = db.Restaurants.Find(restaurantId);
                review.RestaurantId = r.Id;
                review.RestaurantName = r.Name;
            }
            else
            {
                var rev = db.Reviews.Include(p=>p.Restaurant)
                    .Where(p => p.Id == id).FirstOrDefault();
                review.RestaurantId = rev.RestaurantId;
                review.RestaurantName = rev.Restaurant.Name;
                review.Id = rev.Id;
                review.Rating = rev.Rating;
            }
            
            return View(review);
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(Review review)
        {
            if (ModelState.IsValid) 
            {
                review.ReviewDate = DateTime.Now;

                if (Request.IsAuthenticated)
                {
                    review.ReviewerName = User.Identity.GetUserName();
                }

                if (review.Id == 0)
                {
                    db.Reviews.Add(review);
                }
                else
                {
                    var entity = db.Reviews.Find(review.Id);
                    entity.Body = review.Body;
                    entity.Rating = review.Rating;
                }

                db.SaveChanges();
                return RedirectToAction("Index", new { id = review.RestaurantId });
            }

            //ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", review.RestaurantId);
            return View(review);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }

            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", review.RestaurantId);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", review.RestaurantId);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }


        [ChildActionOnly]
        public ActionResult BestReview (ReviewActionVm model)
        {
            IQueryable<Review> review = db.Reviews;
            if (model.RestaurantId.HasValue)
            {
                review = review.Where(p => p.RestaurantId == model.RestaurantId.Value);
            }
            if (model.Type == Enums.ReviewDisplayType.Best)
            {
                model.Review = review.OrderByDescending(p => p.Rating).FirstOrDefault();
                model.Title = "Best Review";
            }
            else if (model.Type == Enums.ReviewDisplayType.Worse)
            {
                model.Review = review.OrderBy(p => p.Rating).
                    ThenByDescending(p => p.ReviewDate).FirstOrDefault();
                model.Title = "Bad Review";
            }
            else
            {
                model.Review = review.OrderByDescending(p => p.ReviewDate).FirstOrDefault();
                model.Title = "Latest Review";
            }
            return PartialView("_Review", model);
        }


        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
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

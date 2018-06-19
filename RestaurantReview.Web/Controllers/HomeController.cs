using RestaurantReview.Core.Models;
using RestaurantReview.DB.DataContext;
using RestaurantReview.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using AutoMapper;

namespace RestaurantReview.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string searchRest)
        {
            //var vm = new List<RestaurantListViewModels>();
            using (var db = new ReviewsDb())
            {
                var rest = db.Restaurants.Include(p => p.Reviews)
                    .Where(p=> searchRest == null || p.Name.Contains(searchRest)).ToList();

                //mapping two lists together
                var vm = Mapper.Map<List<Restaurant>, List<RestaurantListViewModels>>(rest);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Restaurants", vm);
                }
                return View(vm);


                
                //foreach (var r in rest)
                //{
                //    var restList = new RestaurantListViewModels(){

                //        Name = r.Name,
                //        Address = r.Address,
                //        City = r.City,
                //        Country = r.Country,
                //        ImageUrl = r.ImageUrl,
                //        CountOfReviews = r.Reviews.Count(),
                //        AverageScore = r.Reviews.Average(p => p.Rating)
                //    };

                //    };
                //}

            
            }
        }
       

                public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
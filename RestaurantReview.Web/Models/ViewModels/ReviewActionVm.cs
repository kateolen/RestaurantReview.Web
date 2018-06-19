using RestaurantReview.Core.Models;
using RestaurantReview.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReview.Web.Models.ViewModels {
    public class ReviewActionVm {
        public int? RestaurantId { get; set; }       
        public string Title { get; set; }
        public Enums.ReviewDisplayType Type { get; set; }
        public Review Review { get; set; }
    }
}
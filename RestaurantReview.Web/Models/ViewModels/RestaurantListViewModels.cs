using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReview.Web.Models.ViewModels
{
    public class RestaurantListViewModels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string Country { get; set; }
        public string ImageUrl { get; set; }
        public int CountOfReviews { get; set; }
        public double AverageScore { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReview.Core.Models
{
   public class Restaurant
    {
        public Restaurant()
        {
            Reviews = new List<Review>();
            Menues = new List<Menu>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string Country { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Menu> Menues { get; set; }


    }
}

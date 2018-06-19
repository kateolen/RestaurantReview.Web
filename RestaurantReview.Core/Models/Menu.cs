using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReview.Core.Models {
    public class Menu {

        public int Id { get; set; }
        public string Food { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}

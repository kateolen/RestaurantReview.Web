using RestaurantReview.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantReview.Web.Models.ViewModels {
    public class MenuVm {
        public int Id { get; set; }
        public string Food { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(150)]
        public string Description { get; set; }

        [Display(Name = "Price US$")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }

        public Restaurant Restaurant { get; set; }
    }
}
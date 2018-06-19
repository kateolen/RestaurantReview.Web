using RestaurantReview.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReview.DB.DataContext
{
    public class ReviewsDb : DbContext 
    {
        public ReviewsDb() : base("name=DefaultConnection")
        {

        }
            public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Menu> Menues { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>().Property(p => p.Zip).HasMaxLength(10);

            base.OnModelCreating(modelBuilder);
        }
    }
    }


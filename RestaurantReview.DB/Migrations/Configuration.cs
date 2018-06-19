namespace RestaurantReview.DB.Migrations
{
    using RestaurantReview.Core.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RestaurantReview.DB.DataContext.ReviewsDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "RestaurantReview.DB.DataContext.ReviewsDb";
        }

        protected override void Seed(RestaurantReview.DB.DataContext.ReviewsDb context)
        {
            for (int i = 0; i <1000; i++)
            {
                context.Restaurants.AddOrUpdate(r => r.Name,
                    new Restaurant()
                    {
                        Name = "Restaurant " + i,
                        Address = i + " Test ave",
                        City = "Piladelphia",
                        Zip = "19056",
                        Phone = string.Format("(215) 555-{0}", string.Join("", Enumerable.Repeat(i, 4).ToArray()).Substring(0, 4)),
                        Website = $"http://r-{i}.test.com",
                        Country = "USA"

                    });
            }
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}

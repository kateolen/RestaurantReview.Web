namespace RestaurantReview.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedatasize : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Restaurants", "Zip", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Restaurants", "Zip", c => c.String());
        }
    }
}

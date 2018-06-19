namespace RestaurantReview.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingMenuModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Food = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RestaurantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId, cascadeDelete: true)
                .Index(t => t.RestaurantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Menus", "RestaurantId", "dbo.Restaurants");
            DropIndex("dbo.Menus", new[] { "RestaurantId" });
            DropTable("dbo.Menus");
        }
    }
}

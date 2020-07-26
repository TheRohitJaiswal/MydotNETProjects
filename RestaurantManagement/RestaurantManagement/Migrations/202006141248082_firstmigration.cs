namespace RestaurantManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cart",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        userId = c.Int(nullable: false),
                        menuItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuItem", t => t.menuItemId, cascadeDelete: true)
                .Index(t => t.menuItemId);
            
            CreateTable(
                "dbo.MenuItem",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        freeDelivery = c.Boolean(nullable: false),
                        Price = c.Double(nullable: false),
                        dateOfLaunch = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        categoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Category", t => t.categoryId, cascadeDelete: true)
                .Index(t => t.categoryId);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
           
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cart", "menuItemId", "dbo.MenuItem");
            DropForeignKey("dbo.MenuItem", "categoryId", "dbo.Category");
            DropIndex("dbo.MenuItem", new[] { "categoryId" });
            DropIndex("dbo.Cart", new[] { "menuItemId" });
            DropTable("dbo.Category");
            DropTable("dbo.MenuItem");
            DropTable("dbo.Cart");
        }
    }
}

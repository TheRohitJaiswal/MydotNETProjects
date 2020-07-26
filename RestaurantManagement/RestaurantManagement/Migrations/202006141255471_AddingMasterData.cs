namespace RestaurantManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingMasterData : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Category values('Main Course')");
            Sql("Insert into Category values('Starters')");
            Sql("Insert into Category values('Snacks')");
        }
        
        public override void Down()
        {
        }
    }
}

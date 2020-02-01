namespace WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RestructuredTablesMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientRequests", "CustomerId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClientRequests", "CustomerId");
        }
    }
}

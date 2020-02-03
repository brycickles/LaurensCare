namespace SoloCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BackToNonNullableBool : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "HasBeenConsulted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "HasBeenConsulted", c => c.Boolean());
        }
    }
}

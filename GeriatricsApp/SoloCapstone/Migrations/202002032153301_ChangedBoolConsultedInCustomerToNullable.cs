namespace SoloCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedBoolConsultedInCustomerToNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "HasBeenConsulted", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "HasBeenConsulted", c => c.Boolean(nullable: false));
        }
    }
}

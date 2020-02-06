namespace SoloCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCustomerIdToEventsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "CustomerId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "CustomerId");
        }
    }
}

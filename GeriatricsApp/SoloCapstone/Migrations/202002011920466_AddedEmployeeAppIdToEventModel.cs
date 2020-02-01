namespace SoloCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEmployeeAppIdToEventModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "EmpApplicationId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "EmpApplicationId");
        }
    }
}

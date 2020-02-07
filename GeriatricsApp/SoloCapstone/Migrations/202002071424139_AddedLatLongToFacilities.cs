namespace SoloCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLatLongToFacilities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Facilities", "Latitude", c => c.Double(nullable: false));
            AddColumn("dbo.Facilities", "Longitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Facilities", "Longitude");
            DropColumn("dbo.Facilities", "Latitude");
        }
    }
}

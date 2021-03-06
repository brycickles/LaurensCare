namespace SoloCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedImageURLToFacilitiesModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Facilities", "ImageURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Facilities", "ImageURL");
        }
    }
}

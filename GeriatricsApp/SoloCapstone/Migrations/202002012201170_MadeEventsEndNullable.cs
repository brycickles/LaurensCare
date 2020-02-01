namespace SoloCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeEventsEndNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "End", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "End", c => c.DateTime(nullable: false));
        }
    }
}

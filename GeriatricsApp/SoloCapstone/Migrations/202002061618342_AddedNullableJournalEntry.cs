namespace SoloCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNullableJournalEntry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "JournalEntry", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "JournalEntry");
        }
    }
}

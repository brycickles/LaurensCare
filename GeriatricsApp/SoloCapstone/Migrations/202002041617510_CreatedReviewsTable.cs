namespace SoloCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedReviewsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewKey = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.ReviewKey);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Reviews");
        }
    }
}

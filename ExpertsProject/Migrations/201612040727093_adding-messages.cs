namespace ExpertsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingmessages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        UserID = c.String(nullable: false, maxLength: 128),
                        BodyText = c.String(),
                        Date = c.DateTime(nullable: false),
                        IsPartOf = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID, t.UserID })
                .ForeignKey("dbo.Tickets", t => t.IsPartOf, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.IsPartOf);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "IsPartOf", "dbo.Tickets");
            DropIndex("dbo.Messages", new[] { "IsPartOf" });
            DropIndex("dbo.Messages", new[] { "UserID" });
            DropTable("dbo.Messages");
        }
    }
}

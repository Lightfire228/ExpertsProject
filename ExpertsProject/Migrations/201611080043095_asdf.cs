namespace ExpertsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdf : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AttachedUsers",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        TicketID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.TicketID })
                .ForeignKey("dbo.Tickets", t => t.TicketID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.TicketID);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedBy = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.Experts",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Validated = c.Boolean(nullable: false),
                        ExpertiseCatagory = c.String(),
                        KeywordsID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Keywords", t => t.KeywordsID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.KeywordsID);
            
            CreateTable(
                "dbo.Keywords",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Keyword = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        UserID = c.String(nullable: false, maxLength: 128),
                        BodyText = c.String(),
                        Date = c.DateTime(nullable: false),
                        IsPartOf_ID = c.Int(),
                        Ticket_ID = c.Int(),
                    })
                .PrimaryKey(t => new { t.ID, t.UserID })
                .ForeignKey("dbo.Tickets", t => t.IsPartOf_ID)
                .ForeignKey("dbo.Tickets", t => t.Ticket_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.IsPartOf_ID)
                .Index(t => t.Ticket_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "Ticket_ID", "dbo.Tickets");
            DropForeignKey("dbo.Messages", "IsPartOf_ID", "dbo.Tickets");
            DropForeignKey("dbo.Experts", "ID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Experts", "KeywordsID", "dbo.Keywords");
            DropForeignKey("dbo.AttachedUsers", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AttachedUsers", "TicketID", "dbo.Tickets");
            DropForeignKey("dbo.Tickets", "CreatedBy", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "Ticket_ID" });
            DropIndex("dbo.Messages", new[] { "IsPartOf_ID" });
            DropIndex("dbo.Messages", new[] { "UserID" });
            DropIndex("dbo.Experts", new[] { "KeywordsID" });
            DropIndex("dbo.Experts", new[] { "ID" });
            DropIndex("dbo.Tickets", new[] { "CreatedBy" });
            DropIndex("dbo.AttachedUsers", new[] { "TicketID" });
            DropIndex("dbo.AttachedUsers", new[] { "UserID" });
            DropTable("dbo.Messages");
            DropTable("dbo.Keywords");
            DropTable("dbo.Experts");
            DropTable("dbo.Tickets");
            DropTable("dbo.AttachedUsers");
        }
    }
}

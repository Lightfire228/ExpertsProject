namespace ExpertsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Killing_messages_table : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "IsPartOf_ID", "dbo.Tickets");
            DropForeignKey("dbo.Messages", "Ticket_ID", "dbo.Tickets");
            DropForeignKey("dbo.Messages", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "UserID" });
            DropIndex("dbo.Messages", new[] { "IsPartOf_ID" });
            DropIndex("dbo.Messages", new[] { "Ticket_ID" });
            DropTable("dbo.Messages");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => new { t.ID, t.UserID });
            
            CreateIndex("dbo.Messages", "Ticket_ID");
            CreateIndex("dbo.Messages", "IsPartOf_ID");
            CreateIndex("dbo.Messages", "UserID");
            AddForeignKey("dbo.Messages", "UserID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Messages", "Ticket_ID", "dbo.Tickets", "ID");
            AddForeignKey("dbo.Messages", "IsPartOf_ID", "dbo.Tickets", "ID");
        }
    }
}

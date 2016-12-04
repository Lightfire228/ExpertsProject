namespace ExpertsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedtitletotickets : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "Title");
        }
    }
}

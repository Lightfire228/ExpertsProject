namespace ExpertsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedusercontactinfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Street", c => c.String());
            AddColumn("dbo.AspNetUsers", "City", c => c.String());
            AddColumn("dbo.AspNetUsers", "State", c => c.String());
            AddColumn("dbo.AspNetUsers", "Zip", c => c.String());
            DropColumn("dbo.AspNetUsers", "ContactID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ContactID", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "Zip");
            DropColumn("dbo.AspNetUsers", "State");
            DropColumn("dbo.AspNetUsers", "City");
            DropColumn("dbo.AspNetUsers", "Street");
        }
    }
}

namespace ExpertsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdfrt : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Messages");
            AlterColumn("dbo.Messages", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Messages", new[] { "ID", "UserID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Messages");
            AlterColumn("dbo.Messages", "ID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Messages", new[] { "ID", "UserID" });
        }
    }
}

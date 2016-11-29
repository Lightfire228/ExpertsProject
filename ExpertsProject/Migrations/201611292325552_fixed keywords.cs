namespace ExpertsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedkeywords : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Experts", "KeywordsID", "dbo.Keywords");
            DropIndex("dbo.Experts", new[] { "KeywordsID" });
            RenameColumn(table: "dbo.Experts", name: "KeywordsID", newName: "Keyword_ID");
            AddColumn("dbo.Experts", "Keywords", c => c.String());
            AlterColumn("dbo.Experts", "Keyword_ID", c => c.Int());
            CreateIndex("dbo.Experts", "Keyword_ID");
            AddForeignKey("dbo.Experts", "Keyword_ID", "dbo.Keywords", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Experts", "Keyword_ID", "dbo.Keywords");
            DropIndex("dbo.Experts", new[] { "Keyword_ID" });
            AlterColumn("dbo.Experts", "Keyword_ID", c => c.Int(nullable: false));
            DropColumn("dbo.Experts", "Keywords");
            RenameColumn(table: "dbo.Experts", name: "Keyword_ID", newName: "KeywordsID");
            CreateIndex("dbo.Experts", "KeywordsID");
            AddForeignKey("dbo.Experts", "KeywordsID", "dbo.Keywords", "ID", cascadeDelete: true);
        }
    }
}

namespace ExpertsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refixingkeywords : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Experts", "Keyword_ID", "dbo.Keywords");
            DropIndex("dbo.Experts", new[] { "Keyword_ID" });
            DropColumn("dbo.Experts", "Keyword_ID");
            DropTable("dbo.Keywords");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Keywords",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Keyword = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Experts", "Keyword_ID", c => c.Int());
            CreateIndex("dbo.Experts", "Keyword_ID");
            AddForeignKey("dbo.Experts", "Keyword_ID", "dbo.Keywords", "ID");
        }
    }
}

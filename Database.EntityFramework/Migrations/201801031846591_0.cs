namespace Database.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(nullable: false),
                        Url = c.String(),
                        RssSourceId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RssSources", t => t.RssSourceId, cascadeDelete: true)
                .Index(t => t.RssSourceId);
            
            CreateTable(
                "dbo.RssSources",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Url = c.String(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "RssSourceId", "dbo.RssSources");
            DropIndex("dbo.News", new[] { "RssSourceId" });
            DropTable("dbo.RssSources");
            DropTable("dbo.News");
        }
    }
}

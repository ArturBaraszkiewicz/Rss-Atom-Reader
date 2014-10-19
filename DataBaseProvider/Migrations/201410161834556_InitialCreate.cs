namespace DataBaseProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 255),
                        IsActive = c.Boolean(nullable: false),
                        LastSync = c.DateTime(nullable: false),
                        SyncPeriod = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DataProvider",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProviderName = c.String(nullable: false, maxLength: 255),
                        ProviderURI = c.String(nullable: false, maxLength: 255),
                        IsActive = c.Boolean(nullable: false),
                        ProviderType = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        LastSync = c.DateTime(nullable: false),
                        SyncPeriod = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Content",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 255),
                        Content = c.String(maxLength: 255),
                        Author = c.String(maxLength: 255),
                        PublicationDate = c.DateTime(nullable: false),
                        ProviderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DataProvider", t => t.ProviderId, cascadeDelete: true)
                .Index(t => t.ProviderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Content", "ProviderId", "dbo.DataProvider");
            DropForeignKey("dbo.DataProvider", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Content", new[] { "ProviderId" });
            DropIndex("dbo.DataProvider", new[] { "CategoryId" });
            DropTable("dbo.Content");
            DropTable("dbo.DataProvider");
            DropTable("dbo.Categories");
        }
    }
}

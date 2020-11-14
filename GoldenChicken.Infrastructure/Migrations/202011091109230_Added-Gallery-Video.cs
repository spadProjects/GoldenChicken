namespace GoldenChicken.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedGalleryVideo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GalleryVideos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Video = c.String(),
                        Title = c.String(nullable: false),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GalleryVideos");
        }
    }
}

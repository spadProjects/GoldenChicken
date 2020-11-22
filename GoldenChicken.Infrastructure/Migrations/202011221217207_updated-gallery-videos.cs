namespace GoldenChicken.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedgalleryvideos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GalleryVideos", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GalleryVideos", "Image");
        }
    }
}

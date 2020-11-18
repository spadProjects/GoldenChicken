namespace GoldenChicken.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatedtestimonials : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Testimonials", "Role", c => c.String(nullable: false));
            AddColumn("dbo.Testimonials", "Image", c => c.String());
            DropColumn("dbo.Testimonials", "Rate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Testimonials", "Rate", c => c.Int());
            DropColumn("dbo.Testimonials", "Image");
            DropColumn("dbo.Testimonials", "Role");
        }
    }
}

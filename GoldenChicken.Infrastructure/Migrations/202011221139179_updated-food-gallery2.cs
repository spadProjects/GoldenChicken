namespace GoldenChicken.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedfoodgallery2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FoodGalleries", "FoodId", "dbo.Foods");
            DropIndex("dbo.FoodGalleries", new[] { "FoodId" });
            RenameColumn(table: "dbo.FoodGalleries", name: "FoodId", newName: "Food_Id");
            AlterColumn("dbo.FoodGalleries", "Food_Id", c => c.Int());
            AlterColumn("dbo.FoodGalleries", "Title", c => c.String(nullable: false));
            CreateIndex("dbo.FoodGalleries", "Food_Id");
            AddForeignKey("dbo.FoodGalleries", "Food_Id", "dbo.Foods", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FoodGalleries", "Food_Id", "dbo.Foods");
            DropIndex("dbo.FoodGalleries", new[] { "Food_Id" });
            AlterColumn("dbo.FoodGalleries", "Title", c => c.String());
            AlterColumn("dbo.FoodGalleries", "Food_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.FoodGalleries", name: "Food_Id", newName: "FoodId");
            CreateIndex("dbo.FoodGalleries", "FoodId");
            AddForeignKey("dbo.FoodGalleries", "FoodId", "dbo.Foods", "Id", cascadeDelete: true);
        }
    }
}

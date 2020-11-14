namespace GoldenChicken.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedProjectsToFoods : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProjectTypes", newName: "FoodTypes");
            DropForeignKey("dbo.ProjectGalleries", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "ProjectTypeId", "dbo.ProjectTypes");
            DropIndex("dbo.ProjectGalleries", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "ProjectTypeId" });
            CreateTable(
                "dbo.FoodGalleries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        FoodId = c.Int(nullable: false),
                        Title = c.String(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Foods", t => t.FoodId, cascadeDelete: true)
                .Index(t => t.FoodId);
            
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 600),
                        ShortDescription = c.String(),
                        Description = c.String(),
                        Image = c.String(),
                        FoodTypeId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FoodTypes", t => t.FoodTypeId)
                .Index(t => t.FoodTypeId);
            
            DropTable("dbo.ProjectGalleries");
            DropTable("dbo.Projects");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 600),
                        ShortDescription = c.String(),
                        Description = c.String(),
                        Image = c.String(),
                        ProjectTypeId = c.Int(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProjectGalleries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        ProjectId = c.Int(nullable: false),
                        Title = c.String(),
                        InsertUser = c.String(),
                        InsertDate = c.DateTime(),
                        UpdateUser = c.String(),
                        UpdateDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Foods", "FoodTypeId", "dbo.FoodTypes");
            DropForeignKey("dbo.FoodGalleries", "FoodId", "dbo.Foods");
            DropIndex("dbo.Foods", new[] { "FoodTypeId" });
            DropIndex("dbo.FoodGalleries", new[] { "FoodId" });
            DropTable("dbo.Foods");
            DropTable("dbo.FoodGalleries");
            CreateIndex("dbo.Projects", "ProjectTypeId");
            CreateIndex("dbo.ProjectGalleries", "ProjectId");
            AddForeignKey("dbo.Projects", "ProjectTypeId", "dbo.ProjectTypes", "Id");
            AddForeignKey("dbo.ProjectGalleries", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.FoodTypes", newName: "ProjectTypes");
        }
    }
}

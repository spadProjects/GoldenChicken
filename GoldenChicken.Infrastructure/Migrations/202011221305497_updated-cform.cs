namespace GoldenChicken.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedcform : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactForms", "IsViewed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContactForms", "IsViewed");
        }
    }
}

namespace GoldenChicken.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedContactForm2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ContactForms", "Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContactForms", "Phone", c => c.String(nullable: false, maxLength: 600));
        }
    }
}

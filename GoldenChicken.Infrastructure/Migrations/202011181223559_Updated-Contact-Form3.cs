namespace GoldenChicken.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedContactForm3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContactForms", "Message", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContactForms", "Message", c => c.String());
        }
    }
}

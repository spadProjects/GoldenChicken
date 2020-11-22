namespace GoldenChicken.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatedcertificates : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Partners", newName: "Certificates");
            AddColumn("dbo.Certificates", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Certificates", "Description");
            RenameTable(name: "dbo.Certificates", newName: "Partners");
        }
    }
}

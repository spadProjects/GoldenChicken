namespace GoldenChicken.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatedourteam : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OurTeams", "Title", c => c.String(nullable: false));
            AddColumn("dbo.OurTeams", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.OurTeams", "Role", c => c.String(nullable: false));
            DropColumn("dbo.OurTeams", "Name");
            DropColumn("dbo.OurTeams", "Facebook");
            DropColumn("dbo.OurTeams", "Twitter");
            DropColumn("dbo.OurTeams", "Google");
            DropColumn("dbo.OurTeams", "Linkedin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OurTeams", "Linkedin", c => c.String());
            AddColumn("dbo.OurTeams", "Google", c => c.String());
            AddColumn("dbo.OurTeams", "Twitter", c => c.String());
            AddColumn("dbo.OurTeams", "Facebook", c => c.String());
            AddColumn("dbo.OurTeams", "Name", c => c.String());
            AlterColumn("dbo.OurTeams", "Role", c => c.String());
            DropColumn("dbo.OurTeams", "Description");
            DropColumn("dbo.OurTeams", "Title");
        }
    }
}

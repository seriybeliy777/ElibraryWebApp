namespace ELibraryWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Book", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Book", "Description");
        }
    }
}

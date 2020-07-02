namespace ELibraryWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Book", "ImageUrl", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Book", "ImageUrl");
        }
    }
}

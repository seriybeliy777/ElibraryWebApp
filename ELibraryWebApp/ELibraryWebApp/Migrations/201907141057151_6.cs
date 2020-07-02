namespace ELibraryWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Book", "PageCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Book", "PageCount");
        }
    }
}

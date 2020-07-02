namespace ELibraryWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Rating", "Value", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rating", "Value", c => c.Int(nullable: false));
        }
    }
}

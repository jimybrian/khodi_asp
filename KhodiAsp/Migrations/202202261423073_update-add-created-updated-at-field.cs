namespace KhodiAsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateaddcreatedupdatedatfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Properties", "createdAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Properties", "updatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Properties", "updatedAt");
            DropColumn("dbo.Properties", "createdAt");
        }
    }
}

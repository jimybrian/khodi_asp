namespace KhodiAsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateunitstable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Units", "createdAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Units", "updatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Units", "updatedAt");
            DropColumn("dbo.Units", "createdAt");
        }
    }
}

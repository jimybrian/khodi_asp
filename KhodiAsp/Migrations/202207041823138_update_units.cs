namespace KhodiAsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_units : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Units", "deposit", c => c.Single(nullable: false));
            AddColumn("dbo.Units", "numOfRooms", c => c.Int(nullable: false));
            AddColumn("dbo.Units", "blockNumber", c => c.String());
            AddColumn("dbo.Units", "numOfBedrooms", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Units", "numOfBedrooms");
            DropColumn("dbo.Units", "blockNumber");
            DropColumn("dbo.Units", "numOfRooms");
            DropColumn("dbo.Units", "deposit");
        }
    }
}

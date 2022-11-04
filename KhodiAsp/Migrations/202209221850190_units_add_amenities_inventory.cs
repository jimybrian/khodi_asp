namespace KhodiAsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class units_add_amenities_inventory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Units", "amenities", c => c.String());
            AddColumn("dbo.Units", "inventory", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Units", "inventory");
            DropColumn("dbo.Units", "amenities");
        }
    }
}

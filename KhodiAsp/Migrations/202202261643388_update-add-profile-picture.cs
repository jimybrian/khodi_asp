namespace KhodiAsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateaddprofilepicture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "profilePicture", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "profilePicture");
        }
    }
}

namespace KhodiAsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedValidations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assets", "assetName", c => c.String(nullable: false));
            AlterColumn("dbo.Assets", "secureUrl", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "email", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "firstName", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "lastName", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "password", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "phoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "surname", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "surname", c => c.String());
            AlterColumn("dbo.Users", "phoneNumber", c => c.String());
            AlterColumn("dbo.Users", "password", c => c.String());
            AlterColumn("dbo.Users", "lastName", c => c.String());
            AlterColumn("dbo.Users", "firstName", c => c.String());
            AlterColumn("dbo.Users", "email", c => c.String());
            AlterColumn("dbo.Assets", "secureUrl", c => c.String());
            DropColumn("dbo.Assets", "assetName");
        }
    }
}

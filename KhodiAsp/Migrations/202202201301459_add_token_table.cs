namespace KhodiAsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_token_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tokens",
                c => new
                    {
                        tokenId = c.Guid(nullable: false),
                        userId = c.Guid(nullable: false),
                        validity = c.Boolean(nullable: false),
                        createdAt = c.DateTime(nullable: false),
                        updatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.tokenId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tokens");
        }
    }
}

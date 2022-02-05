namespace KhodiAsp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assets",
                c => new
                    {
                        assetId = c.Guid(nullable: false),
                        createdAt = c.DateTime(nullable: false),
                        updatedAt = c.DateTime(nullable: false),
                        propertyId = c.String(),
                        publicId = c.String(),
                        secureUrl = c.String(),
                    })
                .PrimaryKey(t => t.assetId);
            
            CreateTable(
                "dbo.LandlordProperties",
                c => new
                    {
                        propertyId = c.Guid(nullable: false),
                        landlordId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.propertyId);
            
            CreateTable(
                "dbo.Properties",
                c => new
                    {
                        propertyId = c.Guid(nullable: false),
                        amenities = c.String(),
                        county = c.String(),
                        houseRules = c.String(),
                        imagesUrl = c.String(),
                        landmark = c.String(),
                        latitude = c.Single(nullable: false),
                        longitude = c.Single(nullable: false),
                        maxRent = c.Single(nullable: false),
                        minRent = c.Single(nullable: false),
                        noOfBlocks = c.Int(nullable: false),
                        noOfUnits = c.Int(nullable: false),
                        propertyDescription = c.String(),
                        propertyName = c.String(),
                        propertyType = c.String(),
                        town = c.String(),
                        LandlordProperties_propertyId = c.Guid(),
                    })
                .PrimaryKey(t => t.propertyId)
                .ForeignKey("dbo.LandlordProperties", t => t.LandlordProperties_propertyId)
                .Index(t => t.LandlordProperties_propertyId);
            
            CreateTable(
                "dbo.Landlords",
                c => new
                    {
                        landordId = c.Guid(nullable: false),
                        userId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.landordId)
                .ForeignKey("dbo.Users", t => t.userId, cascadeDelete: true)
                .Index(t => t.userId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        userId = c.Guid(nullable: false),
                        createdAt = c.DateTime(nullable: false),
                        updatedAt = c.DateTime(nullable: false),
                        accountExpired = c.Boolean(nullable: false),
                        accountLocked = c.Boolean(nullable: false),
                        credentialsExpired = c.Boolean(nullable: false),
                        email = c.String(),
                        enabled = c.Boolean(nullable: false),
                        firstName = c.String(),
                        lastName = c.String(),
                        password = c.String(),
                        phoneNumber = c.String(),
                        surname = c.String(),
                    })
                .PrimaryKey(t => t.userId);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        permissionId = c.Guid(nullable: false),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.permissionId);
            
            CreateTable(
                "dbo.PropertyAssets",
                c => new
                    {
                        propertyId = c.Guid(nullable: false),
                        assetId = c.Guid(nullable: false),
                        properties_propertyId = c.Guid(),
                    })
                .PrimaryKey(t => t.propertyId)
                .ForeignKey("dbo.Assets", t => t.assetId, cascadeDelete: true)
                .ForeignKey("dbo.Properties", t => t.properties_propertyId)
                .Index(t => t.assetId)
                .Index(t => t.properties_propertyId);
            
            CreateTable(
                "dbo.PropertyUnits",
                c => new
                    {
                        unitId = c.Guid(nullable: false),
                        propertyId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.unitId)
                .ForeignKey("dbo.Properties", t => t.propertyId, cascadeDelete: true)
                .Index(t => t.propertyId);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        unitId = c.Guid(nullable: false),
                        maxRent = c.Single(nullable: false),
                        minRent = c.Single(nullable: false),
                        unitNnumber = c.String(),
                        PropertyUnits_unitId = c.Guid(),
                        TenantUnits_tenantId = c.Guid(),
                    })
                .PrimaryKey(t => t.unitId)
                .ForeignKey("dbo.PropertyUnits", t => t.PropertyUnits_unitId)
                .ForeignKey("dbo.TenantUnits", t => t.TenantUnits_tenantId)
                .Index(t => t.PropertyUnits_unitId)
                .Index(t => t.TenantUnits_tenantId);
            
            CreateTable(
                "dbo.RolePermissions",
                c => new
                    {
                        roleId = c.Guid(nullable: false),
                        permissionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.roleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        roleId = c.Guid(nullable: false),
                        createdAt = c.DateTime(nullable: false),
                        updatedAt = c.DateTime(nullable: false),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.roleId);
            
            CreateTable(
                "dbo.Tenants",
                c => new
                    {
                        tenantId = c.Guid(nullable: false),
                        userId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.tenantId)
                .ForeignKey("dbo.Users", t => t.userId, cascadeDelete: true)
                .Index(t => t.userId);
            
            CreateTable(
                "dbo.TenantUnits",
                c => new
                    {
                        tenantId = c.Guid(nullable: false),
                        unitId = c.Guid(nullable: false),
                        tenants_tenantId = c.Guid(),
                    })
                .PrimaryKey(t => t.tenantId)
                .ForeignKey("dbo.Tenants", t => t.tenants_tenantId)
                .Index(t => t.tenants_tenantId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        userId = c.Guid(nullable: false),
                        roleId = c.Guid(nullable: false),
                        user_userId = c.Guid(),
                    })
                .PrimaryKey(t => t.userId)
                .ForeignKey("dbo.Roles", t => t.roleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.user_userId)
                .Index(t => t.roleId)
                .Index(t => t.user_userId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "user_userId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "roleId", "dbo.Roles");
            DropForeignKey("dbo.Units", "TenantUnits_tenantId", "dbo.TenantUnits");
            DropForeignKey("dbo.TenantUnits", "tenants_tenantId", "dbo.Tenants");
            DropForeignKey("dbo.Tenants", "userId", "dbo.Users");
            DropForeignKey("dbo.Units", "PropertyUnits_unitId", "dbo.PropertyUnits");
            DropForeignKey("dbo.PropertyUnits", "propertyId", "dbo.Properties");
            DropForeignKey("dbo.PropertyAssets", "properties_propertyId", "dbo.Properties");
            DropForeignKey("dbo.PropertyAssets", "assetId", "dbo.Assets");
            DropForeignKey("dbo.Landlords", "userId", "dbo.Users");
            DropForeignKey("dbo.Properties", "LandlordProperties_propertyId", "dbo.LandlordProperties");
            DropIndex("dbo.UserRoles", new[] { "user_userId" });
            DropIndex("dbo.UserRoles", new[] { "roleId" });
            DropIndex("dbo.TenantUnits", new[] { "tenants_tenantId" });
            DropIndex("dbo.Tenants", new[] { "userId" });
            DropIndex("dbo.Units", new[] { "TenantUnits_tenantId" });
            DropIndex("dbo.Units", new[] { "PropertyUnits_unitId" });
            DropIndex("dbo.PropertyUnits", new[] { "propertyId" });
            DropIndex("dbo.PropertyAssets", new[] { "properties_propertyId" });
            DropIndex("dbo.PropertyAssets", new[] { "assetId" });
            DropIndex("dbo.Landlords", new[] { "userId" });
            DropIndex("dbo.Properties", new[] { "LandlordProperties_propertyId" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.TenantUnits");
            DropTable("dbo.Tenants");
            DropTable("dbo.Roles");
            DropTable("dbo.RolePermissions");
            DropTable("dbo.Units");
            DropTable("dbo.PropertyUnits");
            DropTable("dbo.PropertyAssets");
            DropTable("dbo.Permissions");
            DropTable("dbo.Users");
            DropTable("dbo.Landlords");
            DropTable("dbo.Properties");
            DropTable("dbo.LandlordProperties");
            DropTable("dbo.Assets");
        }
    }
}

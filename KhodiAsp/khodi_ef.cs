using KhodiAsp.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace KhodiAsp
{
    public partial class khodi_ef : DbContext
    {
        public khodi_ef()
            : base("name=khodi_ef")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }


        //Adding models
        //public DbSet<ModelName> modelName { get; set; }
        public DbSet<Assets> assets { get; set; }
        public DbSet<LandlordProperties> landlordProperties { get; set; }
        public DbSet<Landlords> landlords { get; set; }
        public DbSet<Permissions> permission { get; set; }
        public DbSet<Properties> propertoes { get; set; }
        public DbSet<PropertyAssets> propertyAssets { get; set; }
        public DbSet<PropertyUnits> propertyUnits { get; set; }
        public DbSet<RolePermissions> rolePermissions { get; set; }
        public DbSet<Roles> roles { get; set; }
        public DbSet<Tenants> tenants { get; set; }
        public DbSet<TenantUnits> tenantUnits { get; set; }
        public DbSet<Units> units { get; set; }
        public DbSet<UserRoles> userRoles { get; set; }
        public DbSet<Users> users { get; set; }

    }
}

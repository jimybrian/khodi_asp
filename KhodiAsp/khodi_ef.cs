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
    }
}

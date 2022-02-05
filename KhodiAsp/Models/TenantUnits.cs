using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhodiAsp.Models
{
    public class TenantUnits
    {
        [Key]
        public Guid tenantId { get; set; }
        public Guid unitId { get; set; }

        public Tenants tenants { get; set; }
        public ICollection<Units> units { get; set; }

    }
}
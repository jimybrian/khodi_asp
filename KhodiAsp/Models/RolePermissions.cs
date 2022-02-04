using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhodiAsp.Models
{
    public class RolePermissions
    {
        [Key]
        public Guid roleId { get; set; }
        public Guid permissionId { get; set; }     
    }
}
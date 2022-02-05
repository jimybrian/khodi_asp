using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhodiAsp.Models
{
    public class Permissions
    {
        [Key]
        public Guid permissionId { get; set; }
        public string name { get; set; }

    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhodiAsp.Models
{
    public class Tenants
    {
        [Key]
        public Guid tenantId { get; set; }
        public Guid userId { get; set; }
        [JsonIgnore]
        public Users user { get; set; }
    }
}
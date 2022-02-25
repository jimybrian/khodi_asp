using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhodiAsp.Models
{
    public class UserRoles
    {
        [Key]
        public Guid userId { get; set; }      
        public Guid roleId { get; set; }
        [JsonIgnore]
        public Users user { get; set; }
        [JsonIgnore]
        public Roles role { get; set; }
    }
}
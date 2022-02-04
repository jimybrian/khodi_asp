using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhodiAsp.Models
{
    public class Landlords
    {
        [Key]
        public Guid landordId { get; set; }
        public Guid userId { get; set; }
    }
}
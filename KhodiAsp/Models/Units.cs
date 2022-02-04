using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhodiAsp.Models
{
    public class Units
    {
        [Key]
        public Guid unitId { get; set; }
        public float maxRent { get; set; }
        public float minRent { get; set; }
        public string unitNnumber { get; set; }

    }
}
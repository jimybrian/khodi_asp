using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhodiAsp.Models
{
    public class LandlordProperties
    {      
        [Key]
        public Guid landlordId { get; set; }
        public Guid propertyId { get; set; }

    }
}
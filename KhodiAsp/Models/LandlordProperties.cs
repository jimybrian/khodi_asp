using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhodiAsp.Models
{
    public class LandlordProperties
    {             
        public Guid landlordId { get; set; }
        [Key]
        public Guid propertyId { get; set; }

        public ICollection<Properties> properties { get; set; }

    }
}
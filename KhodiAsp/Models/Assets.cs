using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhodiAsp.Models
{
    public class Assets
    {
        [Key]
        public Guid assetId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public string propertyId { get; set; }
        public string publicId { get; set; }
        public string secureUrl { get; set; }
    }
}
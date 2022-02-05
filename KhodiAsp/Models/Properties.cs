using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhodiAsp.Models
{
    public class Properties
    {
        [Key]
        public Guid propertyId { get; set; }
        public string amenities { get; set; }
        public string county { get; set; }
        public string houseRules { get; set; }
        public string imagesUrl { get; set; }   
        public string landmark { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public float maxRent { get; set; }
        public float minRent { get; set; }
        public int noOfBlocks { get; set; }
        public int noOfUnits { get; set; }
        public string propertyDescription { get; set; }
        public string propertyName { get; set; }
        public string propertyType { get; set; }
        public string town { get; set; }
    }
}
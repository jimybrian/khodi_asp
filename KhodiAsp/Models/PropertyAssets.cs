using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhodiAsp.Models
{
    public class PropertyAssets
    {
        [Key]
        public Guid propertyId { get; set; }
        public Guid assetId { get; set; }
        [JsonIgnore]
        public Assets assets { get; set; }
        [JsonIgnore]
        public Properties properties { get; set; }    
    }
}
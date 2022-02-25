using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhodiAsp.Models
{
    public class PropertyUnits
    {        
        public Guid propertyId { get; set; }
        [Key]
        public Guid unitId { get; set; }
        [JsonIgnore]
        public ICollection<Units> units { get; set; }
        [JsonIgnore]
        public Properties property { get; set; }


    }
}
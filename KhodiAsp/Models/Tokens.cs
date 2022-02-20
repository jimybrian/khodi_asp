using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhodiAsp.Models
{
    public class Tokens
    {
        [Key]
        public Guid tokenId { get; set; }
        public Guid userId { get; set; }
        public bool validity { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
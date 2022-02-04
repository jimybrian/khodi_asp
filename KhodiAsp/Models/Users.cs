using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhodiAsp.Models
{
    public class Users
    {
        [Key]
        public Guid userId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public bool accountExpired { get; set; }
        public bool accountLocked { get; set; }
        public bool credentialsExpired { get; set; }
        public string email { get; set; }
        public bool enabled { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public string phoneNumber { get; set; }
        public string surName { get; set; }

    }
}
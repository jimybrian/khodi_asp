
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
        [Required]       
        public string email { get; set; }
        public bool enabled { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string phoneNumber { get; set; }
        [Required]
        public string surname { get; set; }

    }
}
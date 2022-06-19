using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhodiAsp.Data
{
    public class LoginItems
    {
        public string email { get; set; }
        public string password { get; set; }    
    }

    public class ForgotPasswordItems : LoginItems
    {
        public string newPassword { get; set; }
    }

    public class UserItems
    {
        public Guid userId { get; set; }
        public string tokenGen { get; set; }
        public string firstName { get; set; }        
        public string lastName { get; set; }
        public string phoneNumber { get; set; }       
        public string surname { get; set; }
        public string email { get; set; }       
        public string profilePic { get; set; }
    }
}
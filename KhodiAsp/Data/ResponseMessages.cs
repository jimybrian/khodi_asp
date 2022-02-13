using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhodiAsp.Data
{
    public class ResponseMessages
    {
        public static string SUCCESS = "success";
        public static string ERROR = "error";
        public static string VERIFY_USER = "UNVERIFIED";
        public static string EXPIRED_TOKEN = "expired_token";
        public static string VERIFICATION_EXPIRED = "VERIFICATION_EXPIRED";
    }
}
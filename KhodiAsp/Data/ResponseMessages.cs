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
        public static string ERROR_USER_NOT_FOUND = "ERROR_USER_NOT_FOUND";
        public static string ERROR_INVALID_CREDENTIALS = "INVALID_CREDENTIALS";
        public static string ERROR_ACCOUNT_LOCKED = "ERROR_ACCOUNT_LOCKED";
        public static string ERROR_ACCOUNT_TOKEN_EXPIRED = "ERROR_ACCOUNT_TOKEN_EXPIRED";       
    }
}
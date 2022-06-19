using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhodiAsp.Data
{
    public class ResponseMessages
    {
        public static string SUCCESS = "SUCCESS";
        public static string ERROR = "ERROR";
        public static string EMAIL_IN_USE = "EMAIL_IN_USE";
        public static string VERIFY_USER = "UNVERIFIED";
        public static string EXPIRED_TOKEN = "EXPIRED_TOKEN";
        public static string VERIFICATION_EXPIRED = "VERIFICATION_EXPIRED";
        public static string ERROR_USER_NOT_FOUND = "ERROR_USER_NOT_FOUND";
        public static string ERROR_INVALID_CREDENTIALS = "INVALID_CREDENTIALS";
        public static string ERROR_ACCOUNT_LOCKED = "ERROR_ACCOUNT_LOCKED";
        public static string ERROR_ACCOUNT_TOKEN_EXPIRED = "ERROR_ACCOUNT_TOKEN_EXPIRED";
        public static string ERROR_CREDENTIALS_EXPIRED = "ERROR_CREDENTIALS_EXPIRED";
    }
}
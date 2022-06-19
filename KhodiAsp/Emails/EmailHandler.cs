using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace KhodiAsp.Emails
{
    public class EmailHandler
    {
        private string emailAddress = "khodikhodi@outlook.com";//"khodi.info@gmail.com";
        private string emailPassword = "EkLC4qzL2zK5MDb";//"gKRMF7TL7RQfTSD";

        public string createPasswordResetEmail(string userName, string email, string password)
        {            
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Emails/password_reset.html")))
            { body = reader.ReadToEnd(); }

            body = body.Replace("{userName}", userName);
            body = body.Replace("{rEmail}", email);

            body = body.Replace("{psWord}", password);

            return body;
        }

        public string createVerificationEmail(string userName, string email, Guid userid)
        {
            var scheme = HttpContext.Current.Request.Url.Scheme;
            var baseUrl = HttpContext.Current.Request.Url.Host;
            var port = HttpContext.Current.Request.Url.Port;

            var fullBaseUrl = scheme + "://" + baseUrl + ":"+ port;

            string url = fullBaseUrl + "/api/users/verfiyUser?email=" + email + "&userId=" + userid;

            string body = string.Empty;
            using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Emails/verify_account.html")))
            { body = reader.ReadToEnd(); }

            body = body.Replace("{userName}", userName);
            body = body.Replace("{rEmail}", email);

            body = body.Replace("{verifUrl}", url);        

            return body;
        }

        public string createVerificationSuccessEmail(string userName, string email)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Emails/verify_success.html")))
            { body = reader.ReadToEnd(); }

            body = body.Replace("{userName}", userName);
            body = body.Replace("{rEmail}", email);          

            return body;
        }


        public void sendEmail(string email, string body, string subject)
        {
            string to = email; //To address    
            string from = emailAddress; //From address    

            MailMessage message = new MailMessage(from, to);

            string mailbody = body;
            message.Subject = subject;
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 587); //Outlook smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential(emailAddress, emailPassword);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
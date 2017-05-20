using System;
using System.Text;
using System.Web.Mail;

namespace V308CMS.Common
{
    public class EmailSender
    {
        private readonly string _gport = "465";
        private readonly string _userName = "cuoivlnet@gmail.com";
        private readonly string _password = "Cuoivlnet123#@!";
        private readonly string _smtpServer = "smtp.gmail.com";

        public EmailSender(string userName, string password, string smtpServer, string port)
        {
            if (!string.IsNullOrWhiteSpace(userName)) {
                _userName = userName;
            }
            if (!string.IsNullOrWhiteSpace(password))
            {
                _password = password;
            }
            if (!string.IsNullOrWhiteSpace(smtpServer))
            {
                _smtpServer = smtpServer;
            }

            if (!string.IsNullOrWhiteSpace(port))
            {
                _gport = port;
            }
           
        }

        public  string SendMail(string from,string to, string subject, string body)
        {
            // Mail initialization 
            var mail = new MailMessage
            {
                From = from,
                To = to,
                Subject = subject,
                BodyFormat = MailFormat.Html,
                BodyEncoding = Encoding.UTF8,
                Body = body
            };
            //mail.Cc = cc;
            // Smtp configuration
            SmtpMail.SmtpServer = _smtpServer;
            // - smtp.gmail.com use smtp authentication
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", "2");    //port to launch send
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", _userName);
            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", _password);
            // - smtp.gmail.com use port 465 or 587

            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", _gport);
            // - smtp.gmail.com use STARTTLS (some call this SSL)

            mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true");
            // try to send Mail


            try
            {
                SmtpMail.Send(mail);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }




    }
}
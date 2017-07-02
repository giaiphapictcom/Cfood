using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V308CMS.Data.Helpers
{
    public class Email
    {
        public static string SendEmail(string to, string subject, string body)
        {
            var emailSender = new V308CMS.Common.EmailSender(
                 ConfigHelper.GMailUserName,
                 ConfigHelper.GMailPassword,
                 ConfigHelper.GmailSmtpServer,
                 ConfigHelper.GMailPort
                );
            return emailSender.SendMail(ConfigHelper.GMailUserName, to,
                 subject, body);
        }
    }
}
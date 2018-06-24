using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Muktas.ERP.Common
{
    public class Email
    {
        public delegate void LogHandler(MailMessage message);
        public event LogHandler Log;

        public bool SendEmail(string Subject, string Body, string To)
        {
            return SendEmail(Subject, Body, To, null, null);
        }
        public bool SendEmail(string Subject, string Body, string To, string CC)
        {
            return SendEmail(Subject, Body, To, CC, null);
        }
        public bool SendEmail(string Subject, string Body, string To, string CC, string BCC)
        {
            using (MailMessage message = new MailMessage())
            {
                message.To.Add(To);
                if (!string.IsNullOrEmpty(CC))
                    message.CC.Add(CC);
                if (!string.IsNullOrEmpty(BCC))
                    message.Bcc.Add(BCC);
                message.Body = Body;
                message.IsBodyHtml = true;
                message.Subject = Subject;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                if (Log != null)
                    Log(message);
                using (SmtpClient client = new SmtpClient())
                {
                    try
                    {
                        client.Send(message);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
            return true;
        }
    }
}

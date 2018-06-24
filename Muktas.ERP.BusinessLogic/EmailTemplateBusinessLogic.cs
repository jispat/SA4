using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Muktas.ERP.BusinessLogic
{
    public enum EmailTypes
    {
        UserActivation = 1,
        ForgotPassword = 2,
        ChangePassword = 3,
        ResetPassword = 4
    }
    public sealed class EmailTemplateBusinessLogic : BaseBusinessLogic<Model.EmailTemplate>
    {
        Data.EmailTemplateData _EmailTemplateData;
        Data.LogData _LogData;
        Common.Email _Email;
        public EmailTemplateBusinessLogic() : base(new Data.EmailTemplateData())
        {
            _EmailTemplateData = new Data.EmailTemplateData();
            _LogData = new Data.LogData();
            _Email = new Common.Email();
            _Email.Log += _Email_Log;
        }

        private void _Email_Log(MailMessage message)
        {
            Model.Log log = new Model.Log();
            log.MessageType = "Email";
            log.Message = message.Subject;
            log.Description = message.Body;
            log.URL = message.To.ToString();
            log.IPAddress = "";            
            _LogData.Add(log);
        }

        private Model.EmailTemplate FindByEmailTemplate(EmailTypes emailTypes, SortedList<string, string> replaceText)
        {
            Model.EmailTemplate emailTemplate = _EmailTemplateData.FindByTemplateName(Enum.GetName(typeof(EmailTypes), emailTypes));
            for(int i=0;i<replaceText.Count();i++)
            {
                emailTemplate.Body = emailTemplate.Body.Replace(replaceText.ElementAt(i).Key, replaceText.ElementAt(i).Value);
            }
            return emailTemplate;
        }
        public void SendUserActivationEmail(Model.User user)
        {
            SortedList<string, string> replaceText = new SortedList<string, string>();
            replaceText.Add("##Code##", user.Code);
            Model.EmailTemplate emailTemplate = FindByEmailTemplate(EmailTypes.UserActivation, replaceText);
            _Email.SendEmail(emailTemplate.Subject, emailTemplate.Body, user.Email);
        }
        public void SendForgotPasswordEmail(Model.User user)
        {
            SortedList<string, string> replaceText = new SortedList<string, string>();
            replaceText.Add("##Code##", user.Code);
            Model.EmailTemplate emailTemplate = FindByEmailTemplate(EmailTypes.ForgotPassword, replaceText);
            _Email.SendEmail(emailTemplate.Subject, emailTemplate.Body, user.Email);
        }
        public void SendChangePasswordEmail(Model.User user)
        {
            SortedList<string, string> replaceText = new SortedList<string, string>();
            Model.EmailTemplate emailTemplate = FindByEmailTemplate(EmailTypes.ChangePassword, replaceText);
            _Email.SendEmail(emailTemplate.Subject, emailTemplate.Body, user.Email);
        }
        public void SendResetPasswordEmail(Model.User user)
        {
            SortedList<string, string> replaceText = new SortedList<string, string>();
            Model.EmailTemplate emailTemplate = FindByEmailTemplate(EmailTypes.ResetPassword, replaceText);
            _Email.SendEmail(emailTemplate.Subject, emailTemplate.Body, user.Email);
        }

    }
}

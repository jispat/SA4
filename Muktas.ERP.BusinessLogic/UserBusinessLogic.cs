using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muktas.ERP.BusinessLogic
{
    public sealed class UserBusinessLogic : BaseBusinessLogic<Model.User>
    {
        Data.UserData _UserData;
        BusinessLogic.EmailTemplateBusinessLogic _EmailTemplateBusinessLogic;
        public UserBusinessLogic() : base(new Data.UserData())
        {
            _UserData = new Data.UserData();
            _EmailTemplateBusinessLogic = new BusinessLogic.EmailTemplateBusinessLogic();
        }
        public new void Add(Model.User user)
        {
            user.Password = Common.Functions.Encrypt(user.Password);
            user.IsActive = true;
            user.UserId = Guid.NewGuid();
            user.CreatedOn = DateTime.Now;
            //user.Code = Guid.NewGuid().ToString();
            _Data.Add(user);
            //_EmailTemplateBusinessLogic.SendUserActivationEmail(user);
        }
        public new void Update(Model.User user)
        {
            if (user.Password == "NotUpdate")
            {
                user.Password = _UserData.FindByID(user.UserId).Password;
            }
            else
            {
                user.Password = Common.Functions.Encrypt(user.Password);
            }
            user.IsActive = true;
            user.UpdatedOn = DateTime.Now;
            //user.Code = Guid.NewGuid().ToString();
            _Data.Update(user);
            //_EmailTemplateBusinessLogic.SendUserActivationEmail(user);
        }

        public Model.User FindByEmail(string email)
        {
            return _UserData.FindByEmail(email);
        }
        public bool MakeUserActive(string code)
        {
            if (_UserData.FindByCode(code) != null)
            {
                _UserData.MakeUserActive(code);
                return true;
            }
            return false;
        }
        public bool ForgotPassword(string email)
        {
            Model.User user = _UserData.FindByEmail(email);
            if (user != null)
            {
                user.Code = Guid.NewGuid().ToString();
                _UserData.SetForgotPasswordCode(email, user.Code);
                _EmailTemplateBusinessLogic.SendForgotPasswordEmail(user);
                return true;
            }
            return false;
        }
        public int ChangePassword(Model.ChangePassword changePassword)
        {
            Model.User user = _UserData.FindByID(changePassword.UserId);
            if (user != null)
            {
                if (user.Password != Common.Functions.Encrypt(changePassword.OldPassword))
                    return -1;

                _UserData.SetNewPassword(user.UserId, Common.Functions.Encrypt(changePassword.NewPassword));
                _EmailTemplateBusinessLogic.SendChangePasswordEmail(user);
                return 1;
            }
            return 0;
        }
        public int ResetPassword(Model.ResetPassword resetPassword)
        {
            Model.User user = _UserData.FindByCode(resetPassword.Code);
            if (user != null)
            {
                _UserData.SetNewPassword(user.UserId, Common.Functions.Encrypt(resetPassword.NewPassword));
                _EmailTemplateBusinessLogic.SendResetPasswordEmail(user);
                return 1;
            }
            return 0;
        }
    }
}

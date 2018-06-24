using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Muktas.ERP.API.Controllers
{
    
    public class UserController : BaseApiController
    {
        BusinessLogic.UserBusinessLogic _userBusinessLogic;
        public UserController() : base()
        {
            _userBusinessLogic = new BusinessLogic.UserBusinessLogic();
            MainBusinessLogic = _userBusinessLogic;
        }

        [HttpPost]
        [ActionFilters.AuthorizationRequired]
        public HttpResponseMessage AddUser(Model.User user)
        {   

            if (user != null && ModelState.IsValid)
            {
                var exist = _userBusinessLogic.FindByField("UserName", user.UserName).Where(x => x.UserId != user.UserId).FirstOrDefault();
                if (exist != null)
                    return ReturnIsExistsMessage("UserName");
                exist = _userBusinessLogic.FindByField("Email", user.Email).Where(x => x.UserId != user.UserId).FirstOrDefault();
                if (exist != null)
                    return ReturnIsExistsMessage("Email");
                _userBusinessLogic.Add(user);
                return ReturnSuccessMessage();
            }
            else
            {
                return ReturnModelValidFailMessage(ModelState, user);
            }
        }
        [HttpPost]
        [ActionFilters.AuthorizationRequired]
        public HttpResponseMessage UpdateUser(Model.User user)
        {
            //ModelState.Remove("user.Password");
            if (user != null && ModelState.IsValid)
            {
                var exist = _userBusinessLogic.FindByField("UserName", user.UserName).Where(x => x.UserId != user.UserId).FirstOrDefault();
                if (exist != null)
                    return ReturnIsExistsMessage("UserName");
                exist = _userBusinessLogic.FindByField("Email", user.Email).Where(x => x.UserId != user.UserId).FirstOrDefault();
                if (exist != null)
                    return ReturnIsExistsMessage("Email");
                
                _userBusinessLogic.Update(user);
                return ReturnSuccessMessage();
            }
            else
            {
                return ReturnModelValidFailMessage(ModelState, user);
            }
        }
        [HttpPost]
        [ActionFilters.AuthorizationRequired]
        public HttpResponseMessage RemoveUser(Guid userId)
        {
            var user = _userBusinessLogic.FindByID(userId);
            if (user == null)
                return ReturnNotFoundMessage(userId);
            _userBusinessLogic.Remove(userId);
            return ReturnSuccessMessage();
        }
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage UserActivation(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return ReturnInvalidArgument(code);
            }
            else if (_userBusinessLogic.MakeUserActive(code))
            {
                return ReturnSuccessMessage();
            }
            else
            {
                return ReturnNotFoundMessage(code);
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage ForgotPassword(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return ReturnInvalidArgument(email);
            }
            else if (_userBusinessLogic.ForgotPassword(email))
            {
                return ReturnSuccessMessage();
            }
            else
            {
                return ReturnNotFoundMessage(email);
            }
        }
        [HttpPost]
        [ActionFilters.AuthorizationRequired]
        public HttpResponseMessage ChangePassword(Model.ChangePassword changePassword)
        {
            if (changePassword != null && ModelState.IsValid)
            {
                int result = _userBusinessLogic.ChangePassword(changePassword);
                if (result == 1)
                    return ReturnSuccessMessage();
                else if (result == 0)
                    return ReturnNotFoundMessage(changePassword);
                else
                    return ReturnCustomMessage(HttpStatusCode.BadRequest, Common.Functions.GetMessage("Old_Password_Not_Matched"));
            }
            else
            {
                return ReturnModelValidFailMessage(ModelState, changePassword);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage CheckResetPasswordCode(string code)
        {
            if (!string.IsNullOrWhiteSpace(code))
            {
                var result = _userBusinessLogic.FindByField("Code",code).FirstOrDefault();
                if (result != null)
                    return ReturnSuccessMessage();
                else
                    return ReturnNotFoundMessage(code);
            }
            else
            {
                return ReturnModelValidFailMessage(ModelState, code);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage ResetPassword(Model.ResetPassword resetPassword)
        {
            if (resetPassword != null && ModelState.IsValid)
            {
                int result = _userBusinessLogic.ResetPassword(resetPassword);
                if (result == 1)
                    return ReturnSuccessMessage();
                else
                    return ReturnNotFoundMessage(resetPassword);
            }
            else
            {
                return ReturnModelValidFailMessage(ModelState, resetPassword);
            }
        }

    }
}

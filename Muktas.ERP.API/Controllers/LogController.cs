using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace Muktas.ERP.API.Controllers
{
    public partial class LogController : BaseApiController
    {
        BusinessLogic.LogBusinessLogic _logBusinessLogic;
        public LogController() : base()
        {
            _logBusinessLogic = new BusinessLogic.LogBusinessLogic();
            MainBusinessLogic = _logBusinessLogic;
        }

        [HttpPost]
        //[AuthorizationRequired]
        public HttpResponseMessage AddLog(Model.Log Model)
        {
            Model.CreatedOn = DateTime.Now;
            Model.URL = Request.RequestUri.AbsoluteUri.ToString();
            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Request != null)
                Model.IPAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
            else
                Model.IPAddress = "N/A";
            Model.MessageType = "Web";
            if (Model != null && ModelState.IsValid)
            {
                _logBusinessLogic.Add(Model);
                return ReturnSuccessMessage();
            }
            else
            {
                return ReturnModelValidFailMessage(ModelState, Model);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace Muktas.ERP.API.ActionFilters
{
    public class GlobalExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {            
            var exceptionType = context.Exception.GetType();

            AddLog(context);

            if (exceptionType == typeof(ValidationException))
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(context.Exception.Message), ReasonPhrase = "ValidationException", };
                throw new HttpResponseException(resp);
            }
            else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                throw new HttpResponseException(context.Request.CreateResponse(HttpStatusCode.Unauthorized));
            }
            else
            {
                throw new HttpResponseException(context.Request.CreateResponse(HttpStatusCode.InternalServerError));
            }
        }
        private void AddLog(HttpActionExecutedContext context)
        {
            Model.Log log = new Model.Log();
            log.Message = context.Exception.Message;
            log.Description = context.Exception.Source + " - " + context.Exception.StackTrace;
            log.URL = context.Request.RequestUri.AbsoluteUri.ToString();
            if (context.Request.Properties.Where(x => x.Key == "UserId").Count() > 0)
                log.UserId = Guid.Parse(context.Request.Properties["UserId"].ToString());
            log.IPAddress = GetClientIp(context.Request);
            log.Data = context.Request.Content.ReadAsStringAsync().Result;
            (new BusinessLogic.LogBusinessLogic()).Add(log);
        }
        private string GetClientIp(HttpRequestMessage request = null)
        {
            //request = request ?? Request;

            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            //else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            //{
            //    RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
            //    return prop.Address;
            //}
            else if (HttpContext.Current != null)
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                return null;
            }
        }

    }
}
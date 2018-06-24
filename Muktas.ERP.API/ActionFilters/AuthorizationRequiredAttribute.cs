using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Muktas.ERP.API.ActionFilters
{
    public class AuthorizationRequiredAttribute : ActionFilterAttribute
    {
        private const string Token = "Token";
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            var provider = new BusinessLogic.TokenBusinessLogic();
            if (filterContext.Request.Headers.Contains(Token))
            {
                var tokenValue = filterContext.Request.Headers.GetValues(Token).First();
                // Validate Token
                if (provider != null)
                {
                    Guid userId = provider.ValidateToken(tokenValue);
                    filterContext.Request.Properties.Add(new KeyValuePair<string, object>("UserId", userId));
                    if (userId == Guid.Empty)
                    {
                        AddLog(filterContext);
                        var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Invalid Request" };
                        filterContext.Response = responseMessage;
                    }
                    else
                    {
                        string controllerName = filterContext.Request.GetRouteData().Values["controller"].ToString();
                        string actionName = filterContext.Request.GetRouteData().Values["action"].ToString();
                        //if (!provider.CheckUserPermission(userId,controllerName,actionName))
                        //{
                        //    AddLog(filterContext);
                        //    var responseMessage = new HttpResponseMessage(HttpStatusCode.Forbidden) { ReasonPhrase = "Permission denied" };
                        //    filterContext.Response = responseMessage;
                        //}
                    }
                }
            }
            else
            {
                filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            base.OnActionExecuting(filterContext);

        }

        private void AddLog(HttpActionContext context)
        {
            Model.Log log = new Model.Log();
            log.Message = "Unauthorized";
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
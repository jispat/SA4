using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Muktas.ERP.API.Controllers
{
    [Filters.ApiAuthenticationFilter(false)]
    public class AuthenticateController : BaseApiController
    {
        BusinessLogic.TokenBusinessLogic _tokenBusinessLogic ;
        public AuthenticateController() : base()
        {
            _tokenBusinessLogic = new BusinessLogic.TokenBusinessLogic();
            MainBusinessLogic = _tokenBusinessLogic;
        }

        [Route("login")]
        [Route("authenticate")]
        [Route("get/token")]
        [HttpPost]
        public HttpResponseMessage Login(string userName, string password)
        {
            Guid userId = _tokenBusinessLogic.Authenticate(userName, password);
            if (userId == Guid.Empty)
                return new HttpResponseMessage(HttpStatusCode.OK);
                //return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            return GetAuthToken(userId);
        }
        ///// <summary>
        /// Authenticates user and returns token with expiry.
        /// </summary>
        /// <returns></returns>
        //[HttpPost("login")]
        //[POST("authenticate")]
        //[POST("get/token")]
        // pass the basic authorization with username and password to get token
        [HttpPost]
        //[Route("login")]
        //[Route("authenticate")]
        //[Route("get/token")]
        public HttpResponseMessage Authenticate()
        {
            if (System.Threading.Thread.CurrentPrincipal != null && System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                var basicAuthenticationIdentity = System.Threading.Thread.CurrentPrincipal.Identity as Filters.BasicAuthenticationIdentity;
                if (basicAuthenticationIdentity != null)
                {
                    var userId = basicAuthenticationIdentity.UserId;
                    return GetAuthToken(userId);
                }
            }
            return null;
        }

        /// <summary>
        /// Returns auth token for the validated user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private HttpResponseMessage GetAuthToken(Guid userId)
        {
            var token = _tokenBusinessLogic.GenerateToken(userId);
            var response = Request.CreateResponse(HttpStatusCode.OK, (new BusinessLogic.UserBusinessLogic()).FindByID(userId));
            response.Headers.Add("Token", token.AuthToken);
            response.Headers.Add("TokenExpiry", ConfigurationManager.AppSettings["AuthTokenExpiry"]);
            response.Headers.Add("Access-Control-Expose-Headers", "Token,TokenExpiry");
            return response;
        }
    }
}

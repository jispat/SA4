using System; 
 using System.Collections.Generic; 
 using System.Linq; 
 using System.Net; 
 using System.Net.Http; 
 using System.Web.Http; 
 using Muktas.ERP.Model; 
 using Muktas.ERP.API.ActionFilters;  
 namespace Muktas.ERP.API.Controllers { 
     public partial class TokenController : BaseApiController 
     { 
         BusinessLogic.TokenBusinessLogic _TokenBusinessLogic; 
         public TokenController() 
         { 
             _TokenBusinessLogic = new BusinessLogic.TokenBusinessLogic(); 
         } 
         [HttpGet] 
         [AuthorizationRequired] 
         public HttpResponseMessage GetToken(int TokenId) 
         { 
             var obj = _TokenBusinessLogic.FindByID(TokenId); 
             if (obj != null) 
                 return ReturnSuccessMessage(obj); 
             else 
                 return ReturnNotFoundMessage(TokenId); 
         } 
         [HttpGet]  
         [AuthorizationRequired]  
         public HttpResponseMessage GetActiveTokens()  
         {  
             var list = _TokenBusinessLogic.FindAllActive();  
             if (list != null)  
                 return ReturnSuccessMessage(list);  
             else  
                 return ReturnNotFoundMessage(null);  
         }  
 [HttpGet] 
         [AuthorizationRequired] 
         public HttpResponseMessage GetTokens() 
         { 
             var list = _TokenBusinessLogic.FindAll(); 
             if (list != null) 
                 return ReturnSuccessMessage(list); 
             else 
                 return ReturnNotFoundMessage(null); 
         } 
         [HttpPost] 
         [AuthorizationRequired] 
         public HttpResponseMessage AddToken(Model.Token Model) 
         { 
             if (Model != null && ModelState.IsValid) 
             { 
                 if (_TokenBusinessLogic.FindByField("TokenId",Model.TokenId) != null) 
                     return ReturnIsExistsMessage("TokenId"); 
                 _TokenBusinessLogic.Add(Model); 
                 return ReturnSuccessMessage(); 
             } 
             else 
             { 
                 return ReturnModelValidFailMessage(ModelState, Model); 
             } 
         } 
         [HttpPost] 
         [AuthorizationRequired] 
         public HttpResponseMessage UpdateToken(Model.Token Model) 
         { 
             if (Model != null && ModelState.IsValid) 
              { 
                 _TokenBusinessLogic.Update(Model); 
                 return ReturnSuccessMessage(); 
             } 
             else 
             { 
                 return ReturnModelValidFailMessage(ModelState, Model); 
             } 
         } 
         [HttpPost] 
         [AuthorizationRequired] 
         public HttpResponseMessage RemoveToken(int TokenId) 
         { 
             var obj = _TokenBusinessLogic.FindByID(TokenId); 
             if (obj == null)  
                 return ReturnNotFoundMessage(TokenId); 
             _TokenBusinessLogic.Remove(TokenId); 
             return ReturnSuccessMessage(); 
         } 
     } 
 }

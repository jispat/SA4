using System; 
 using System.Collections.Generic; 
 using System.Linq; 
 using System.Net; 
 using System.Net.Http; 
 using System.Web.Http; 
 using Muktas.ERP.Model; 
 using Muktas.ERP.API.ActionFilters;  
 namespace Muktas.ERP.API.Controllers { 
     public partial class CountryController : BaseApiController 
     { 
         Muktas.ERP.BusinessLogic.CountryBusinessLogic _CountryBusinessLogic; 
         public CountryController() 
         { 
             _CountryBusinessLogic = new BusinessLogic.CountryBusinessLogic(); 
 
            MainBusinessLogic = _CountryBusinessLogic ; 
         } 
         [HttpPost] 
         [AuthorizationRequired] 
         public HttpResponseMessage AddCountry(Muktas.ERP.Model.Country Model) 
         { 
             if (Model != null && ModelState.IsValid) 
             {
                var exist = _CountryBusinessLogic.FindByField("CountryCode", Model.CountryCode).Where(x => x.CountryId != Model.CountryId).FirstOrDefault();
                if (exist != null)
                    return ReturnIsExistsMessage("CountryCode");
                exist = _CountryBusinessLogic.FindByField("CountryName", Model.CountryName).Where(x => x.CountryId != Model.CountryId).FirstOrDefault();
                if (exist != null)
                    return ReturnIsExistsMessage("CountryName");
                _CountryBusinessLogic.Add(Model); 
                 return ReturnSuccessMessage(); 
             } 
             else 
             { 
                 return ReturnModelValidFailMessage(ModelState, Model); 
             } 
         } 
          [HttpPost] 
         [AuthorizationRequired] 
         public HttpResponseMessage UpdateCountry(Muktas.ERP.Model.Country Model) 
         { 
             if (Model != null && ModelState.IsValid) 
              {
                var exist = _CountryBusinessLogic.FindByField("CountryCode", Model.CountryCode).Where(x => x.CountryId != Model.CountryId).FirstOrDefault();
                if (exist != null)
                    return ReturnIsExistsMessage("CountryCode");
                exist = _CountryBusinessLogic.FindByField("CountryName", Model.CountryName).Where(x => x.CountryId != Model.CountryId).FirstOrDefault();
                if (exist != null)
                    return ReturnIsExistsMessage("CountryName");
                _CountryBusinessLogic.Update(Model); 
                 return ReturnSuccessMessage(); 
             } 
             else 
             { 
                 return ReturnModelValidFailMessage(ModelState, Model); 
             } 
         } 
         [HttpPost] 
         [AuthorizationRequired] 
         public HttpResponseMessage RemoveCountry(Guid CountryId) 
         { 
             var obj = _CountryBusinessLogic.FindByID(CountryId); 
             if (obj == null)  
                 return ReturnNotFoundMessage(CountryId); 
             _CountryBusinessLogic.Remove(CountryId); 
             return ReturnSuccessMessage(); 
         } 
     } 
 }

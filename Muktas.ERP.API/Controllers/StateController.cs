using System; 
 using System.Collections.Generic; 
 using System.Linq; 
 using System.Net; 
 using System.Net.Http; 
 using System.Web.Http; 
 using Muktas.ERP.Model; 
 using Muktas.ERP.API.ActionFilters;  
 namespace Muktas.ERP.API.Controllers { 
     public partial class StateController : BaseApiController 
     { 
         Muktas.ERP.BusinessLogic.StateBusinessLogic _StateBusinessLogic; 
         public StateController() 
         { 
             _StateBusinessLogic = new BusinessLogic.StateBusinessLogic(); 
 
            MainBusinessLogic = _StateBusinessLogic ; 
         } 
         [HttpPost] 
         [AuthorizationRequired] 
         public HttpResponseMessage AddState(Muktas.ERP.Model.State Model) 
         { 
             if (Model != null && ModelState.IsValid) 
             {
                var exist = _StateBusinessLogic.FindByField("StateName", Model.StateName).Where(x => x.CountryId == Model.CountryId).Where(x => x.StateId != Model.StateId).FirstOrDefault();
                if (exist != null)
                    return ReturnIsExistsMessage("StateName with same country");
                exist = _StateBusinessLogic.FindByField("StateCode", Model.StateCode).Where(x => x.CountryId == Model.CountryId).Where(x => x.StateId != Model.StateId).FirstOrDefault();
                if (exist != null)
                    return ReturnIsExistsMessage("StateCode with same country");
                exist = _StateBusinessLogic.FindByField("StateVatTinNo", Model.StateVatTinNo).Where(x => x.CountryId == Model.CountryId).Where(x => x.StateId != Model.StateId).FirstOrDefault();
                if (exist != null)
                    return ReturnIsExistsMessage("StateVatTinNo with same country");
                _StateBusinessLogic.Add(Model); 
                 return ReturnSuccessMessage(); 
             } 
             else 
             { 
                 return ReturnModelValidFailMessage(ModelState, Model); 
             } 
         } 
          [HttpPost] 
         [AuthorizationRequired] 
         public HttpResponseMessage UpdateState(Muktas.ERP.Model.State Model) 
         { 
             if (Model != null && ModelState.IsValid) 
              {
                var exist = _StateBusinessLogic.FindByField("StateName", Model.StateName).Where(x => x.CountryId == Model.CountryId).Where(x => x.StateId != Model.StateId).FirstOrDefault();
                if (exist != null)
                    return ReturnIsExistsMessage("StateName with same country");
                exist = _StateBusinessLogic.FindByField("StateCode", Model.StateCode).Where(x => x.CountryId == Model.CountryId).Where(x => x.StateId != Model.StateId).FirstOrDefault();
                if (exist != null)
                    return ReturnIsExistsMessage("StateCode with same country");
                exist = _StateBusinessLogic.FindByField("StateVatTinNo", Model.StateVatTinNo).Where(x => x.CountryId == Model.CountryId).Where(x => x.StateId != Model.StateId).FirstOrDefault();
                if (exist != null)
                    return ReturnIsExistsMessage("StateVatTinNo with same country");
                _StateBusinessLogic.Update(Model); 
                 return ReturnSuccessMessage(); 
             } 
             else 
             { 
                 return ReturnModelValidFailMessage(ModelState, Model); 
             } 
         } 
         [HttpPost] 
         [AuthorizationRequired] 
         public HttpResponseMessage RemoveState(Guid StateId) 
         { 
             var obj = _StateBusinessLogic.FindByID(StateId); 
             if (obj == null)  
                 return ReturnNotFoundMessage(StateId); 
             _StateBusinessLogic.Remove(StateId); 
             return ReturnSuccessMessage(); 
         } 
     } 
 }

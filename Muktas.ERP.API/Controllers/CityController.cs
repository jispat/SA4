using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Muktas.ERP.Model;
using Muktas.ERP.API.ActionFilters;
namespace Muktas.ERP.API.Controllers
{
    public partial class CityController : BaseApiController
    {
        Muktas.ERP.BusinessLogic.CityBusinessLogic _CityBusinessLogic;
        public CityController()
        {
            _CityBusinessLogic = new BusinessLogic.CityBusinessLogic();

            MainBusinessLogic = _CityBusinessLogic;
        }
        [HttpGet]
        [AuthorizationRequired]
        public HttpResponseMessage FindByState(Guid StateId)
        {
            return ReturnSuccessMessage(_CityBusinessLogic.FindByField("StateId", StateId));
        }
        [HttpPost]
        [AuthorizationRequired]
        public HttpResponseMessage AddCity(Muktas.ERP.Model.City Model)
        {
            if (Model != null && ModelState.IsValid)
            {
                var exist = _CityBusinessLogic.FindByField("CityName", Model.CityName).Where(x => x.StateId == Model.StateId && x.Pincode == Model.Pincode).Where(x => x.CityId != Model.CityId).FirstOrDefault();
                if (exist != null)
                    return ReturnIsExistsMessage("CityName with same state & zipcode");
                _CityBusinessLogic.Add(Model);
                return ReturnSuccessMessage();
            }
            else
            {
                return ReturnModelValidFailMessage(ModelState, Model);
            }
        }
        [HttpPost]
        [AuthorizationRequired]
        public HttpResponseMessage UpdateCity(Muktas.ERP.Model.City Model)
        {
            if (Model != null && ModelState.IsValid)
            {
                var exist = _CityBusinessLogic.FindByField("CityName", Model.CityName).Where(x => x.StateId == Model.StateId && x.Pincode == Model.Pincode).Where(x => x.CityId != Model.CityId).FirstOrDefault();
                if (exist != null)
                    return ReturnIsExistsMessage("CityName with same state & zipcode");
                _CityBusinessLogic.Update(Model);
                return ReturnSuccessMessage();
            }
            else
            {
                return ReturnModelValidFailMessage(ModelState, Model);
            }
        }
        [HttpPost]
        [AuthorizationRequired]
        public HttpResponseMessage RemoveCity(Guid CityId)
        {
            var obj = _CityBusinessLogic.FindByID(CityId);
            if (obj == null)
                return ReturnNotFoundMessage(CityId);
            _CityBusinessLogic.Remove(CityId);
            return ReturnSuccessMessage();
        }
    }
}

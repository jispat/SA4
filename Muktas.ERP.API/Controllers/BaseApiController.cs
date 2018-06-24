using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace Muktas.ERP.API.Controllers
{
    [ActionFilters.GlobalExceptionAttribute]
    public abstract partial class BaseApiController : ApiController
    {
        protected BusinessLogic.IBaseBusinessLogicGet<Model.BaseModel> MainBusinessLogic;
        public BaseApiController() { }

        [HttpGet]
        [ActionFilters.AuthorizationRequired]
        public virtual HttpResponseMessage FindByID(Guid Id)
        {
            var obj = MainBusinessLogic.FindByID(Id);
            if (obj != null)
                return ReturnSuccessMessage(obj);
            else
                return ReturnNotFoundMessage(Id);
        }
        [HttpGet]
        [ActionFilters.AuthorizationRequired]
        public virtual HttpResponseMessage FindAllActive()
        {
            var list = MainBusinessLogic.FindAllActive();
            if (list != null)
                return ReturnSuccessMessage(list);
            else
                return ReturnNotFoundMessage(null);
        }
        [HttpGet]
        [ActionFilters.AuthorizationRequired]
        public virtual HttpResponseMessage FindAll()
        {
            var list = MainBusinessLogic.FindAll();
            if (list != null)
                return ReturnSuccessMessage(list);
            else
                return ReturnNotFoundMessage(null);
        }

    }

    public abstract partial class BaseApiController : ApiController
    {
        protected HttpResponseMessage ReturnModelValidFailMessage(ModelStateDictionary modelStates, object value)
        {

            if (value == null)
                return ReturnInvalidArgument(value);
            string Errors = "";
            foreach (ModelState modelState in modelStates.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    if(!string.IsNullOrWhiteSpace(error.ErrorMessage))
                    Errors += error.ErrorMessage + "; ";
                }
            }
            Errors = Errors.Length > 0 ? Errors.Substring(0, Errors.Length - 2) : Errors;
            return ReturnCustomMessage(HttpStatusCode.BadRequest, Errors);
        }
        protected HttpResponseMessage ReturnInvalidArgument(object value)
        {
            return ReturnCustomMessage(HttpStatusCode.BadRequest, Common.Functions.GetMessage("Invalid_Argument"));
        }
        protected HttpResponseMessage ReturnNotFoundMessage(object value)
        {
            return ReturnCustomMessage(HttpStatusCode.NotFound, string.Format(Common.Functions.GetMessage("Not_Found"),value));
        }
        protected HttpResponseMessage ReturnCustomMessage(HttpStatusCode code, object value)
        {
            if (value == null)
                return Request.CreateResponse(code);
            else
                return Request.CreateResponse(code, value);
        }
        protected HttpResponseMessage ReturnIsExistsMessage(string fieldName)
        {
            return ReturnCustomMessage(HttpStatusCode.BadRequest, string.Format(Common.Functions.GetMessage("Already_Exists"), fieldName));
        }
        protected HttpResponseMessage ReturnSuccessMessage(object str)
        {
            return ReturnCustomMessage(HttpStatusCode.OK, str);
        }
        protected HttpResponseMessage ReturnSuccessMessage()
        {
            return ReturnCustomMessage(HttpStatusCode.OK, null);
        }
        protected HttpResponseMessage ReturnSuccessMessage(Model.BaseModel model)
        {
            return ReturnCustomMessage(HttpStatusCode.OK, model);
        }
        protected HttpResponseMessage ReturnSuccessMessage(IEnumerable<Model.BaseModel> list)
        {
            return ReturnCustomMessage(HttpStatusCode.OK, list);
        }
        protected void RemoveModelValidation(string className,string propertyName)
        {
            foreach (var k in ModelState.Values)
            {
                foreach (var l in k.Errors)
                {
                    if (l.Exception.Message.Contains(className) && l.Exception.Message.Contains(propertyName))
                    { k.Errors.Remove(l); return; }                        
                }
            }
        }
        protected void RemoveNestedModelValidation(dynamic Model)
        {
            PropertyInfo[] props = Model.GetType().GetProperties();
            foreach (var p in props.Where(x => x.PropertyType.BaseType != null && x.PropertyType.BaseType.Name == "BaseModel"))
            {
                var klist = ModelState.Keys.Where(x => x.Contains("." + p.PropertyType.Name)).Select(x => x).ToArray<string>();
                foreach (var k in klist)
                {
                    if (ModelState.ContainsKey(k) && (k.IndexOf("." + p.PropertyType.Name + ".") != -1 || k.EndsWith("." + p.PropertyType.Name)))
                        ModelState.Remove(k);
                }
            }
            //"List`1"
            foreach (var p in props.Where(x => x.PropertyType.Name == "List`1" || x.PropertyType.Name == "IEnumerable`1"))
            {
                var klist = ModelState.Keys.Where(x => x.Contains("." + p.Name)).Select(x => x).ToArray<string>();
                foreach (var k in klist)
                {
                    if (ModelState.ContainsKey(k) && (k.IndexOf("." + p.Name + "[") != -1 || k.EndsWith("." + p.Name)))
                        ModelState.Remove(k);
                }
            }

        }
    }
}

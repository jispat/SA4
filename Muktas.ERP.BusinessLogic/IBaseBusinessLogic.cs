using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muktas.ERP.BusinessLogic
{
    public interface IBaseBusinessLogicSet<in T> where T : Model.BaseModel
    {
        void Add(T item);
        void Update(T item);
        void Remove(object Id);

    }
    public interface IBaseBusinessLogicGet<out T> where T : Model.BaseModel
    {
        T FindByID(dynamic id);
        IEnumerable<T> FindAll();
        IEnumerable<T> FindByField(string fieldName, dynamic value);
        IEnumerable<T> FindAllActive();
    }

}

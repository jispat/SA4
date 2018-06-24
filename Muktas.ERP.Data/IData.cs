using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muktas.ERP.Data
{
    public interface IData<T> where T : Model.BaseModel
    {
        dynamic Add(T item);
        void Remove(object id);
        void Update(T item);
        T FindByID(dynamic id);
        IEnumerable<T> FindAll();
        IEnumerable<T> FindAllActive();
        IEnumerable<T> FindByField(string fieldName, dynamic value);
    }

}

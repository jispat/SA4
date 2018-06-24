using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muktas.ERP.BusinessLogic
{
    public abstract class BaseBusinessLogic<T>: IBaseBusinessLogicGet<T>, IBaseBusinessLogicSet<T> where T : Model.BaseModel
    {
        protected Data.IData<T> _Data;
        public BaseBusinessLogic(Data.IData<T> Data)
        {
            _Data = (Data.BaseData<T>)Data;
        }
        public void Add(T item)
        {
            _Data.Add((T)item);
        }

        public IEnumerable<T> FindAll()
        {
            return _Data.FindAll();
        }

        public T FindByID(dynamic id)
        {
            return _Data.FindByID(id);
        }
        public IEnumerable<T> FindByField(string fieldName, dynamic value)
        {
            return _Data.FindByField(fieldName, value);
        }

        public void Remove(object Id)
        {
            _Data.Remove(Id);
        }

        public void Update(T item)
        {
            _Data.Update(item);
        }

        public IEnumerable<T> FindAllActive()
        {
            return _Data.FindAllActive();
        }
    }
}

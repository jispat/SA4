using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muktas.ERP.Data
{
    public abstract class BaseData<T> : IData<T> where T : Model.BaseModel
    {
        protected readonly string _tableName;
        protected readonly string _PKName;
        internal IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConfigurationManager.ConnectionStrings["DBConStr"].ConnectionString);
            }
        }

        public BaseData(string tableName, string PKName)
        {
            _tableName = "[" + tableName + "]";
            _PKName = PKName;
        }

        internal virtual dynamic Mapping(T item)
        {
            return item;
        }

        public virtual object Add(T item)
        {
            using (IDbConnection cn = Connection)
            {
                var parameters = (object)Mapping(item);
                cn.Open();
                return cn.Insert<object>(_tableName, parameters, _PKName);
            }
        }

        public virtual void Update(T item)
        {
            using (IDbConnection cn = Connection)
            {
                var parameters = (object)Mapping(item);
                cn.Open();
                cn.Update(_tableName, parameters, _PKName);
            }
        }

        public virtual void Remove(object id)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Execute(string.Format("DELETE FROM " + _tableName + " WHERE {0}=@id", _PKName), new { id = id });
            }
        }

        public virtual T FindByID(dynamic id)
        {
            T item = default(T);

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                item = cn.Query<T>(string.Format("SELECT * FROM " + _tableName + " WHERE {0}=@Id", _PKName), new { Id = id }).SingleOrDefault();
            }

            return item;
        }

        //public virtual T FindByField(string fieldName, dynamic value)
        //{
        //    T item = default(T);

        //    using (IDbConnection cn = Connection)
        //    {
        //        cn.Open();
        //        item = cn.Query<T>(string.Format("SELECT * FROM " + _tableName + " WHERE {0}=@Value", fieldName), new { Value = value } ).FirstOrDefault();
        //    }

        //    return item;
        //}
        public virtual IEnumerable<T> FindByField(string fieldName, dynamic value)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                return cn.Query<T>(string.Format("SELECT * FROM " + _tableName + " WHERE {0}=@Value", fieldName), new { Value = value });
            }
        }

        public virtual IEnumerable<T> FindAll()
        {
            IEnumerable<T> items = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                items = cn.Query<T>("SELECT * FROM " + _tableName + " ORDER BY 2 ");
            }

            return items;
        }
        public virtual IEnumerable<T> FindAllActive()
        {
            IEnumerable<T> items = null;
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                if (typeof(T).GetProperty("IsActive") != null)
                { items = cn.Query<T>("SELECT * FROM " + _tableName + " WHERE IsActive = 1"); }
                else
                { items = cn.Query<T>("SELECT * FROM " + _tableName); }
            }
            return items;
        }
    }
}

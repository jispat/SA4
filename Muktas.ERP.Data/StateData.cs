using Dapper;
using Muktas.ERP.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Muktas.ERP.Data
{
    public sealed class StateData : BaseData<State>, IData<State>
    {
        public StateData() : base("State", "StateId") { }

        public new IEnumerable<Model.State> FindAll()
        {
            IEnumerable<Model.State> items = null;
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var Query = string.Format("SELECT S.*, C.* FROM " + _tableName + " AS S INNER JOIN Country C ON C.CountryId = S.CountryId ");
                items = cn.Query<Model.State, Model.Country, Model.State>(Query, (S, C) => { S.Country = C; return S; }, splitOn: "CountryId");                
            }
            return items;
        }

    }
}

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
    public sealed class CityData : BaseData<City>, IData<City>
    {
        public CityData() : base("City", "CityId") { }
        public new IEnumerable<Model.City> FindAll()
        {
            IEnumerable<Model.City> items = null;
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var Query = string.Format("SELECT C.*, S.*, CR.* FROM " + _tableName + " AS C INNER JOIN State S ON C.StateId = S.StateId INNER JOIN Country CR ON CR.CountryId = S.CountryId ");
                items = cn.Query<Model.City, Model.State, Model.Country, Model.City>(Query, (C, S, CR) => { S.Country = CR; C.State = S; return C; }, splitOn: "StateId,CountryId");
            }
            return items;
        }

    }
}

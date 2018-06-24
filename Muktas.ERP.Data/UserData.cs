using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace Muktas.ERP.Data
{
    public sealed class UserData : BaseData<Model.User>, IData<Model.User>
    {
        public UserData() : base("User", "UserId") { }

        public Model.User FindByEmail(string email)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                return cn.Query<Model.User>(string.Format("SELECT * FROM " + _tableName + " WHERE Email = @Email"), new { Email = email }).FirstOrDefault();
            }
        }

        public Model.User FindByCode(string code)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                return cn.Query<Model.User>(string.Format("SELECT * FROM " + _tableName + " WHERE Code = @Code"), new { Code = code }).FirstOrDefault();
            }
        }
        public void MakeUserActive(string code)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Execute(string.Format("UPDATE " + _tableName + " SET UpdatedOn = getdate(), IsActive = 1, Code = null WHERE Code = @Code"), new { Code = code });
            }
        }
        public void SetForgotPasswordCode(string email, string code)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Execute(string.Format("UPDATE " + _tableName + " SET UpdatedOn = getdate(), Code = @Code WHERE Email = @Email"), new { Email = email, Code = code });
            }
        }

        public void SetNewPassword(Guid userId, string password)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Execute(string.Format("UPDATE " + _tableName + " SET UpdatedOn = getdate(), Password = @Password, Code = null WHERE UserId = @UserId"), new { UserId = userId, Password = password });
            }
        }
        public new void Remove(object id)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Execute(string.Format("Update " + _tableName + " SET UpdatedOn = getdate(), isactive = CASE WHEN isactive = 1 THEN 0 ELSE 1 END WHERE {0}=@id; DELETE FROM token Where UserId = @id", _PKName), new { id = id });
            }
        }
    }

}

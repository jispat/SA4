using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muktas.ERP.Data
{

    public sealed class TokenData : BaseData<Model.Token>, IData<Model.Token>
    {
        public TokenData() : base("Token", "TokenId") { }

        public Model.User Authenticate(string userName, string password)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                return cn.Query<Model.User>(string.Format("SELECT * FROM [User] WHERE IsActive = 1 and UserName=@UserName and Password=@Password"), new { UserName = userName, Password = password }).SingleOrDefault();
            }
        }
        public Model.Token GenerateToken(Guid userId)
        {
            Model.Token token = new Model.Token();
            token.UserId = userId;
            token.IssuedOn = DateTime.Now;
            token.ExpiresOn = DateTime.Now.AddSeconds(double.Parse(System.Configuration.ConfigurationManager.AppSettings["AuthTokenExpiry"].ToString()));
            token.AuthToken = Guid.NewGuid().ToString();
            base.Add(token);
            return token;
        }
        public Guid ValidateToken(string authTokenId)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var token = cn.Query<Model.Token>(string.Format("SELECT * FROM " + _tableName + " WHERE ExpiresOn > Getdate() and AuthToken=@AuthTokenId"), new { AuthTokenId = authTokenId }).FirstOrDefault();
                if (token == null)
                    return Guid.Empty;
                else
                    return token.UserId;
            }
        }
        public bool KillToken(string authTokenId)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                return cn.Execute(string.Format("DELETE FROM " + _tableName + " WHERE AuthToken={1}", authTokenId)) > 0 ? true : false;
            }
        }
    }
}

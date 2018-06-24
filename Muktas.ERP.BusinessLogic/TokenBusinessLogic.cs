using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muktas.ERP.BusinessLogic
{
    public sealed class TokenBusinessLogic : BaseBusinessLogic<Model.Token>
    {
        Data.TokenData _tokenData;
        public TokenBusinessLogic() : base(new Data.TokenData())
        {
            _tokenData = new Data.TokenData();
        }
        public Guid Authenticate(string userName, string password)
        {
            if (password != null)
                password = Common.Functions.Encrypt(password);
            var user = _tokenData.Authenticate(userName, password);
            if (user != null)
                return user.UserId;
            return Guid.Empty;
        }
        public Guid ValidateToken(string authTokenId)
        {
            return _tokenData.ValidateToken(authTokenId);
        }

        public bool KillToken(string authTokenId)
        {
            return _tokenData.KillToken(authTokenId);
        }

        public Model.Token GenerateToken(Guid userId)
        {
            return _tokenData.GenerateToken(userId);
        }
    }
}

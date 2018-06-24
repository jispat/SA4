using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muktas.ERP.BusinessLogic
{
    public sealed class LogBusinessLogic : BaseBusinessLogic<Model.Log>
    {
        public LogBusinessLogic() : base(new Data.LogData())
        {
        }
    }
}
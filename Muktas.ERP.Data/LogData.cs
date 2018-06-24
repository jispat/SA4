using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muktas.ERP.Data
{
    public sealed class LogData : BaseData<Model.Log>, IData<Model.Log>
    {
        public LogData() : base("Log", "LogId") { }
    }
}

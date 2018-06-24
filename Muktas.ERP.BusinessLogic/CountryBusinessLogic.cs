using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Muktas.ERP.Model;
using Muktas.ERP.Data;
namespace Muktas.ERP.BusinessLogic
{
    public sealed class CountryBusinessLogic : BaseBusinessLogic<Country>
    {
        public CountryBusinessLogic() : base(new CountryData()) { }
    }
}

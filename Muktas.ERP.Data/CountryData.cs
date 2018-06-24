using Muktas.ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Muktas.ERP.Data { public sealed class CountryData : BaseData<Country>, IData<Country> {
        public CountryData() : base("Country", "CountryId") { } } }

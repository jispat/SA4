using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muktas.ERP.Data
{
    public sealed class EmailTemplateData : BaseData<Model.EmailTemplate>, IData<Model.EmailTemplate>
    {
        public EmailTemplateData() : base("EmailTemplate", "EmailTemplateId") { }

        public Model.EmailTemplate FindByTemplateName(string templateName)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                return cn.Query<Model.EmailTemplate>(string.Format("SELECT * FROM " + _tableName + " WHERE TemplateName = @TemplateName"), new { TemplateName = templateName }).FirstOrDefault();
            }
        }
    }
}

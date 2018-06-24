﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muktas.ERP.Model
{
    public class EmailTemplate : BaseModel
    {
        public Guid EmailTemplateId { get; set; }
        public string TemplateName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

    }
}

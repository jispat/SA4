using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muktas.ERP.Model
{
    public class Log : BaseModel
    {
        public Guid LogId { get; set; }
        public string MessageType { get; set; } = "Error";
        public string Message { get; set; }
        public string URL { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public Nullable<Guid> UserId { get; set; }
        public string Description { get; set; }
        public string Data { get; set; }
        public string IPAddress { get; set; }
    }
}

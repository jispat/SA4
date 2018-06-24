using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muktas.ERP.Model
{
    public class ChangePassword : BaseModel
    {
        [Required(ErrorMessageResourceType = typeof(Common.Messages), ErrorMessageResourceName = "Required")]
        public Guid UserId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Common.Messages), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required(ErrorMessageResourceType = typeof(Common.Messages), ErrorMessageResourceName = "Required")]
        [StringLength(20, ErrorMessageResourceType = typeof(Common.Messages), ErrorMessageResourceName = "Invalid_Password", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}

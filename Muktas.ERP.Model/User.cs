using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muktas.ERP.Model
{
    public class User : BaseModel
    {
        public Guid UserId { get; set; }
        [Required( ErrorMessageResourceType = typeof(Common.Messages), ErrorMessageResourceName = "Required")]
        [MaxLength(25)]
        public string UserName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Common.Messages), ErrorMessageResourceName = "Required")]
        [MaxLength(50)]
        public string FullName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Common.Messages), ErrorMessageResourceName = "Required")]
        [MaxLength(10)]
        [Phone(ErrorMessageResourceType = typeof(Common.Messages), ErrorMessageResourceName = "Invalid_Phone")]
        public string Mobile { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Common.Messages), ErrorMessageResourceName = "Required")]
        //[EmailAddress(ErrorMessageResourceType = typeof(Common.Messages), ErrorMessageResourceName = "Invalid_Email") ]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required(ErrorMessageResourceType = typeof(Common.Messages), ErrorMessageResourceName = "Required")]
        [StringLength(20, ErrorMessageResourceType = typeof(Common.Messages), ErrorMessageResourceName = "Invalid_Password", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string Code { get; set; }
        public DateTime CreatedOn { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }


    }
}


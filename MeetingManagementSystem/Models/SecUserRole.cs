using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MeetingManagementSystem.Models
{
    [Table("SecUserRoles")]
    public class SecUserRole
    {
        public int SecUserRoleId { get; set; }
        public virtual SecRole SecRole { get; set; }
        public int SecRoleId { get; set; }
        public virtual SecUser SecUser { get; set; }
        public int SecUserId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
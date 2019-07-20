using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetingManagementSystem.Models
{
    [Table("SecRoles")]
    public class SecRole
    {
        public int SecRoleId { get; set; }

        [Index(IsUnique = true)]
        [Required(ErrorMessage = "Role name is required.")]
        [StringLength(50)]
        public string RoleName { get; set; }
        public bool Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MeetingManagementSystem.Models
{
    [Table("SecUsers")]
    public class SecUser
    {
        public int SecUserId { get; set; }

        [Index(IsUnique = true)]
        [Required(ErrorMessage = "Login name is required.")]
        [StringLength(50)]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public bool Status { get; set; }

        [Required(ErrorMessage = "Email ID is required.")]
        [StringLength(254)]
        public string EmailId { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
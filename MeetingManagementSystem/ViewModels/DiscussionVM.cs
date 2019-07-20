using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MeetingManagementSystem.Models
{
    public class DiscussionVM
    {
        public int DiscussionId { get; set; }
        [NotMapped]
        public string DiscussionText { get; set; }
        public int MeetingId { get; set; }
        public virtual Meeting Meeting { get; set; }
        public int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
   
}
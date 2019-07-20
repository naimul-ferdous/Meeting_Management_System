using System;
using System.ComponentModel.DataAnnotations;


namespace MeetingManagementSystem.Models
{
    public class MeetingResult
    {
        [Key]
       
        public int MeetingResultId { get; set; }
        public string Announcement { get; set; }
        public string Result { get; set; }
        public string Status { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public int MeetingId { get; set; }
        public virtual Meeting Meeting { get; set; }

        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}

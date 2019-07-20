using System;

namespace MeetingManagementSystem.Models
{
    public class MeetingMember
    {
        public int MeetingMemberId { get; set; }
        public int MeetingId { get; set; }
        public virtual Meeting Meeting { get; set; }
        public DateTime BeginningTime { set; get; }
        public DateTime EndTime { set; get; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public int Attendance { get; set; }

        public bool IsDeleted { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}

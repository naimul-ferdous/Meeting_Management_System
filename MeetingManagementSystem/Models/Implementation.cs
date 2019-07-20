using System;

namespace MeetingManagementSystem.Models
{
    public class Implementation
    {
       
        public int ImplementationId { get; set; }
        public string ImplementationDescription { get; set; }
        public int MeetingId { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingManagementSystem.ViewModels
{
    public class MeetingVM
    {
        public int MeetingId { get; set; }
        public string MeetingName { get; set; }
        public DateTime BeginningDate { get; set; }
        public DateTime BeginningTime { set; get; }
        public DateTime EndDate { get; set; }
        public DateTime EndTime { set; get; }
        public string MeetingDescription { get; set; }
        public int Status { get; set; }
        public int EmployeeId { get; set; }
        public bool Approval { get; set; }
        public int VenueId { get; set; }

        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

    }
}
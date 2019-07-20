using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MeetingManagementSystem.Models;

namespace MeetingManagementSystem.ViewModels
{
    public class ShowMeetingDetailsVM
    {
        public int Id { get; set; }
        public int MeetingId { get; set; }
        public virtual Meeting Meeting { get; set; }
        public IEnumerable<MeetingFileUpload> meetingFileUploads { get; set; }
        //public IEnumerable<MeetingMemberVM> meetingMembers { get; set; }
        public AddMemberVM meetingMembers { get; set; }

        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public virtual IEnumerable<MeetingMemberVM> AvailableEmployees { get; set; }

        public IEnumerable<Discussion> Discussions { get; set; }
        public IEnumerable<Implementation> Implementations { get; set; }
    }
}
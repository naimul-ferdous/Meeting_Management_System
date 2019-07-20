using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MeetingManagementSystem.Models;

namespace MeetingManagementSystem.ViewModels
{
    public class MeetingLoggerVM
    {
        public int MeetingLoggerId { get; set; }
        public int MeetingId { get; set; }
        public virtual Meeting Meeting { get; set; }
        public int VenueId { get; set; }
        public virtual Venue Venue { get; set; }
        //public DateTime BeginningTime { set; get; }
        //public DateTime EndTime { set; get; }
        //public string Status { get; set; }

        //public List<Employee> InternalMembers { get; set; }
        //public List<ExternalMember> ExternalMembers { get; set; }
    }
}
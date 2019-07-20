using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MeetingManagementSystem.Models;

namespace MeetingManagementSystem.ViewModels
{
    public class AddMemberVM
    {
        public int Id { get; set; }
        public int MeetingId { get; set; }
        public Meeting Meeting { get; set; }
        public virtual IEnumerable<MeetingMemberVM> AvailableEmployees { get; set; }
        public virtual IEnumerable<MeetingMemberVM> AvailableExternalMembers { get; set; }
    }
}
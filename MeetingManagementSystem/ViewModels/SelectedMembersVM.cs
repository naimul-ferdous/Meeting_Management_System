using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MeetingManagementSystem.Models;

namespace MeetingManagementSystem.ViewModels
{
    public class SelectedMembersVM
    {
        public int MeetingId { get; set; }
        public int EmployeeId { get; set; }
    }
}
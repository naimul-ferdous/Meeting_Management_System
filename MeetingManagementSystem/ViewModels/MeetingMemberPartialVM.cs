using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MeetingManagementSystem.Models;

namespace MeetingManagementSystem.ViewModels
{
    public class MeetingMemberPartialVM
    {
        public int MeetingId { get; set; }
        public int EmployeeId { get; set; }

        public string Department { get; set; }

        public string Designation { get; set; }

        public string EmployeeName { get; set; }
        public string EmployeeType { get; set; }

    }
}
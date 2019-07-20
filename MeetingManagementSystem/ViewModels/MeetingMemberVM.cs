using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MeetingManagementSystem.Models;

namespace MeetingManagementSystem.ViewModels
{
    public class MeetingMemberVM
    {
        public int MeetingId { get; set; }
        //public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
       
        //public int DesignationId { get; set; }
        public virtual Designation Designation { get; set; }

        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        // public bool IsSelected { get; set; }

        public DateTime BeginningTime { set; get; }
        public DateTime EndTime { set; get; }
        public bool IsBusy { get; set; }
    }
}
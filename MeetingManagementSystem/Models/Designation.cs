using System;
using System.Collections.Generic;

namespace MeetingManagementSystem.Models
{
    public class Designation
    {
        public int DesignationId { set; get; }
        public string DesignationName { set; get; }
        public ICollection<Employee>Employees { get; set; }
    }
}

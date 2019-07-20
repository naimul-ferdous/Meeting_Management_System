using System.Collections.Generic;

namespace MeetingManagementSystem.Models
{
    public class Department
    {
        public int DepartmentId { set; get; }
        public string DepartmentName { set; get; }
        public ICollection<Employee> Employees { get; set; }
    }
}

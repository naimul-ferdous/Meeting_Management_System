using System.Collections.Generic;

namespace MeetingManagementSystem.Models
{
    public class EmployeeType
    {
        public int EmployeeTypeId { get; set; }
        public string EmployeeTypeName { get; set; }
        public string EmployeeTypeDescription { get; set; }
        public ICollection<Employee>Employees { get; set; }

    }
}

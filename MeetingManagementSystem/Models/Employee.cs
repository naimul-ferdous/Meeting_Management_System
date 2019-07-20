using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetingManagementSystem.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { set; get; }
        //[Required]
        //[Index(IsUnique = true)]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public string EmployeeOfficialId { set; get; }
        public string EmployeeName { set; get; }
        public string Email { set; get; }
        public string PhoneNumber { set; get; }
        public string Address { set; get; }
        public string Password { set; get; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public ICollection<MeetingResult> MeetingResults { get; set; }
        public ICollection<Meeting> Meetings { get; set; }
        public int OfficeId { get; set; }
        public virtual Office Office { get; set; }
        public int DesignationId { get; set; }
        public virtual Designation Designation { get; set; }
        public int EmployeeTypeId { get; set; }
        public virtual EmployeeType EmployeeType { get; set; }
        public ICollection<Implementation>Implementations { get; set; }
        public bool IsActive { get; set; }
    }
}

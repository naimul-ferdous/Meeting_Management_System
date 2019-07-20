using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetingManagementSystem.Models;

namespace MeetingManagementSystem.ViewModels
{
    public class EmployeeVM
    {
        [Key]
        public int Id { set; get; }
        public int? EmployeeId { set; get; }
        //[Required]
        //[Index(IsUnique = true)]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        [Remote("IsOfficialIdAvailable", "Employee", HttpMethod = "POST", ErrorMessage = "This Official Id Already Exist.", AdditionalFields = "EmployeeId")]
        [Display(Name = "Official Id")]
        public string EmployeeOfficialId { set; get; }
        [Required]
        [Display(Name = "Name")]
        public string EmployeeName { set; get; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [Remote("IsEmailAlreadyExist", "Employee", HttpMethod = "POST", ErrorMessage = "This Email Id Already Exist.", AdditionalFields = "EmployeeId")]
        public string Email { get; set; }
        [Required]
        [Phone]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "{0} must be Number")]
        [RegularExpression(@"^(\+)?[0-9]*$", ErrorMessage = "Invalid Phone number")]
        //[StringLength(11, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { set; get; }
        public string Address { set; get; }
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        //public virtual Department Department { get; set; }
        [Display(Name = "Office")]
        public int OfficeId { get; set; }
        //public virtual Office Office { get; set; }
        [Display(Name = "Designation")]
        public int DesignationId { get; set; }
        //public virtual Designation Designation { get; set; }
        [Display(Name = "Employee Type")]
        public int EmployeeTypeId { get; set; }
        //public virtual EmployeeType EmployeeType { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}
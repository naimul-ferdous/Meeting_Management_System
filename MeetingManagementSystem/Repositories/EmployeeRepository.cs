using System.Collections.Generic;
using System.Linq;
using MeetingManagementSystem.Models;
using System.Data.Entity;

namespace MeetingManagementSystem.Repositories
{
    public class EmployeeRepository
    {
        MeetingManagementDbContext context = new MeetingManagementDbContext();
        
        public int Insert(Employee employee)
        {
            context.Employees.Add(employee);
            return context.SaveChanges();
        }
        public int Edit(Employee employee)
        {
            var emp = context.Employees.Where(e => e.EmployeeId == employee.EmployeeId).FirstOrDefault();

            emp.Address = employee.Address;
            emp.OfficeId = employee.OfficeId;
            emp.DepartmentId = employee.DepartmentId;
            emp.DesignationId = employee.DesignationId;
            emp.Email = employee.Email;
            emp.EmployeeName = employee.EmployeeName;
            emp.EmployeeOfficialId = employee.EmployeeOfficialId;
            emp.EmployeeTypeId = employee.EmployeeTypeId;
            emp.PhoneNumber = employee.PhoneNumber;
            emp.Password = employee.Password;
            emp.IsActive = employee.IsActive;
            context.Entry(emp).State = EntityState.Modified;
            return context.SaveChanges();
        }

        public List<Employee> GetAll()
        {
            return context.Employees.ToList();
        }

        public Employee Get(int id)
        {
            return context.Employees.SingleOrDefault(e => e.EmployeeId == id);
        }
        public int Delete(int employeeId)
        {
            var employee = context.Employees.FirstOrDefault(c => c.EmployeeId == employeeId);
            context.Entry(employee).State = EntityState.Deleted;
            int rowAff = context.SaveChanges();
            return rowAff;
        }


    }

}
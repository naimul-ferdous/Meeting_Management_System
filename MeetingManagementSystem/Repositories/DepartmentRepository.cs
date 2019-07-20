using System.Collections.Generic;
using System.Linq;
using MeetingManagementSystem.Models;
using System.Data.Entity;

namespace MeetingManagementSystem.Repositories
{
    public class DepartmentRepository
    {
        MeetingManagementDbContext context = new MeetingManagementDbContext();
        public int Insert(Department department)
        {
            context.Departments.Add(department);
            return context.SaveChanges();
        }
        public int Edit(Department department)
        {
            context.Entry(department).State = EntityState.Modified;
            return context.SaveChanges();
        }
            public int Delete(int departmentId)
        {
            Department department = new Department() { DepartmentId = departmentId };
            context.Entry(department).State = EntityState.Deleted;
            int rowAff = context.SaveChanges();
            return rowAff;
        }
        public Department Get(int id)
        {
            return context.Departments.SingleOrDefault(d => d.DepartmentId == id);
        }
        public List<Department> GetAll()
        {

            return context.Departments.ToList();            
        }

    }
}
using System.Collections.Generic;
using System.Linq;
using MeetingManagementSystem.Models;

namespace MeetingManagementSystem.Repositories
{
    public class EmployeeTypeRepository
    {
        MeetingManagementDbContext context = new MeetingManagementDbContext();
        public int Insert(EmployeeType employeeType)
        {
            context.EmployeeTypes.Add(employeeType);
            return context.SaveChanges();
        }
        public int Edit(EmployeeType employeeType)
        {
            context.Entry(employeeType).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges();
        }
        public int Delete(int id)
        {
            EmployeeType employeeType = context.EmployeeTypes.SingleOrDefault(e => e.EmployeeTypeId == id);
            context.EmployeeTypes.Remove(employeeType);
            return context.SaveChanges();
        }
        public EmployeeType Get(int id)
        {
            return context.EmployeeTypes.SingleOrDefault(e => e.EmployeeTypeId == id);
        }
        public List<EmployeeType> GetAll()
        {
            return context.EmployeeTypes.ToList();
        }

        public List<EmployeeType> GetEmployeeType()
        {
            var list = (from empType in context.EmployeeTypes
                        select new EmployeeType
                        {
                            EmployeeTypeId=empType.EmployeeTypeId,
                            EmployeeTypeName=empType.EmployeeTypeName
                        }).ToList();

            return list;
        }
    }
}
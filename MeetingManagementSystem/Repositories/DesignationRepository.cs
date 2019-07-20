using System.Collections.Generic;
using System.Linq;
using MeetingManagementSystem.Models;
using System.Data.Entity;

namespace MeetingManagementSystem.Repositories
{
    public class DesignationRepository
    {
        MeetingManagementDbContext context = new MeetingManagementDbContext();
       public int Insert(Designation designation)
        {
            context.Designations.Add(designation);
            return context.SaveChanges();
        }
        public int Edit(Designation designation)
        {
            context.Entry(designation).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges();
        }
        public int Delete(int designationId)
        {
            Designation designation = new Designation() { DesignationId = designationId };
            context.Entry(designation).State = EntityState.Deleted;
            return context.SaveChanges();
        }
        public Designation Get(int id)
        {
            return context.Designations.SingleOrDefault(d => d.DesignationId == id);
        }
        public List<Designation> GetAll()
        {
            return context.Designations.ToList();
        }
    }
}
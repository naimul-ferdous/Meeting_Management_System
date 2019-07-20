using System.Collections.Generic;
using System.Linq;
using MeetingManagementSystem.Models;

namespace MeetingManagementSystem.Repositories
{
    public class EntertainmentRepository
    {
        MeetingManagementDbContext context = new MeetingManagementDbContext();
        public int Insert(Entertainment entertainment)
        {
            context.Entertainments.Add(entertainment);
            return context.SaveChanges();
        }
        public int Edit(Entertainment entertainment)
        {
            context.Entry(entertainment).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges();
        }
        public int Delete(int id)
        {
            Entertainment entertainment = context.Entertainments.Find(id);
            context.Entertainments.Remove(entertainment);
            return context.SaveChanges();
        }
        public Entertainment Get(int id)
        {
            return context.Entertainments.Find(id);
        }
        public List<Entertainment> GetAll()
        {
            return context.Entertainments.ToList();
        }
    }
}
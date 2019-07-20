using System.Collections.Generic;
using System.Linq;
using MeetingManagementSystem.Models;

namespace MeetingManagementSystem.Repositories
{
    public class LogisticRepository
    {
        MeetingManagementDbContext context = new MeetingManagementDbContext();
        public int Insert(Logistic logistic)
        {
            context.Logistics.Add(logistic);
            return context.SaveChanges();
        }
        public int Edit(Logistic logistic)
        {
            context.Entry(logistic).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges();
        }

        public int Delete(int id)
        {
            Logistic logistic = context.Logistics.SingleOrDefault(l => l.LogisticId == id);
            context.Logistics.Remove(logistic);
            return context.SaveChanges();
        }
        public Logistic Get(int id)
        {
            return context.Logistics.SingleOrDefault(l => l.LogisticId == id);
        }
        public List<Logistic> GetAll()
        {
            return context.Logistics.ToList();
        }
    }
}
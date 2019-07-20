using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MeetingManagementSystem.Models;

namespace MeetingManagementSystem.Repositories
{
    public class ImplementationRepository
    {
        MeetingManagementDbContext context = new MeetingManagementDbContext();

        public int Insert(Implementation implementation)
        {
            context.Implementations.Add(implementation);
            return context.SaveChanges();
        }
        public int Edit(Implementation implementation)
        {
            var imp = context.Implementations.FirstOrDefault(c => c.ImplementationId == implementation.ImplementationId);

            if (imp != null)
            {
                context.Implementations.Attach(imp);
                imp.ModifiedDate = implementation.ModifiedDate;
                imp.ModifiedById = implementation.ModifiedById;
                imp.EmployeeId = implementation.EmployeeId;
                imp.ImplementationDescription = implementation.ImplementationDescription;
            }
            //context.Entry(implementation).State = EntityState.Modified;
            return context.SaveChanges();
        }

        public int Delete(int implementationId)
        {
            var implementation = context.Implementations.FirstOrDefault(c => c.ImplementationId == implementationId);
            context.Entry(implementation).State = EntityState.Deleted;
            return context.SaveChanges();
        }
        public Implementation Get(int id)
        {
            return context.Implementations.SingleOrDefault(i => i.ImplementationId == id);
        }
        public List<Implementation> GetAll()
        {
            return context.Implementations.ToList();
        }
        public List<Implementation> GetAllByMeetingId(int meetingId)
        {
            var implementationList = context.Implementations.Where(c => c.MeetingId == meetingId).ToList();
            return implementationList;
        }
    }
}
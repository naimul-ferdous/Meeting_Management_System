using System.Collections.Generic;
using System.Linq;
using MeetingManagementSystem.Models;
using System.Data.Entity;

namespace MeetingManagementSystem.Repositories
{
    public class ExternalMemberRepository
    {
        MeetingManagementDbContext context = new MeetingManagementDbContext();
        public int Insert(ExternalMember externalMember)
        {
            context.ExternalMembers.Add(externalMember);
            return context.SaveChanges();
        }
        public int Edit(ExternalMember externalMember)
        {
            context.Entry(externalMember).State =EntityState.Modified;
            return context.SaveChanges();
        }
        public int Delete(int id)
        {
            ExternalMember externalMember = context.ExternalMembers.SingleOrDefault(em => em.ExternalMemberId == id);
            context.ExternalMembers.Remove(externalMember);
            return context.SaveChanges();
        }
        public ExternalMember Get(int id)
        {
            return context.ExternalMembers.SingleOrDefault(a => a.ExternalMemberId == id);
        }
        public List<ExternalMember> GetAll()
        {
            return context.ExternalMembers.ToList();
        }
    }
}
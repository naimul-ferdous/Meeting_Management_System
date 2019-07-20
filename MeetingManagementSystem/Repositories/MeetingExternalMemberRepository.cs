using System.Collections.Generic;
using System.Linq;
using MeetingManagementSystem.Models;

namespace MeetingManagementSystem.Repositories
{
    public class MeetingExternalMemberRepository
    {
        MeetingManagementDbContext context = new MeetingManagementDbContext();
        public int Insert(MeetingExternalMember meetingExternalMember)
        {
            context.MeetingExternalMembers.Add(meetingExternalMember);
            return context.SaveChanges();
        }
        public int Edit(MeetingExternalMember meetingExternalMember)
        {
            context.Entry(meetingExternalMember).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges();
        }
        public int Delete(int id)
        {
            MeetingExternalMember meetingExternalMember = context.MeetingExternalMembers.SingleOrDefault(me => me.MeetingExternalMemberId == id);
            context.MeetingExternalMembers.Remove(meetingExternalMember);
            return context.SaveChanges();
        }
        public MeetingExternalMember Get(int id)
        {
            return context.MeetingExternalMembers.SingleOrDefault(me => me.MeetingExternalMemberId == id);
        }
        public List<MeetingExternalMember> GetAll()
        {
            return context.MeetingExternalMembers.ToList();
        }
    }
}
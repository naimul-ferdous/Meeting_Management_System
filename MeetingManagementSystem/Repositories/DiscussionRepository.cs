using System.Collections.Generic;
using System.Linq;
using MeetingManagementSystem.Models;
using System.Data.Entity;

namespace MeetingManagementSystem.Repositories
{
    public class DiscussionRepository
    {
        MeetingManagementDbContext context = new MeetingManagementDbContext();
        public int Insert(Discussion discussion)
        {
            context.Discussions.Add(discussion);
            return context.SaveChanges();
        }
        public int Edit(Discussion discussion)
        {
            var ds = context.Discussions.FirstOrDefault(c => c.DiscussionId == discussion.DiscussionId);

            context.Discussions.Attach(ds);
            ds.ModifiedById = discussion.ModifiedById;
            ds.ModifiedDate = discussion.ModifiedDate;
            ds.DiscussionText = discussion.DiscussionText;
           
            return context.SaveChanges();
        }
            public int Delete(int discussionId)
        {
            var ds = context.Discussions.FirstOrDefault(c => c.DiscussionId == discussionId);
            context.Discussions.Remove(ds);
            return context.SaveChanges();
        }
        public Discussion Get(int id)
        {
            return context.Discussions.FirstOrDefault(d => d.DiscussionId == id);
        }
        public List<Discussion> GetAllByMeetingId(int meetingId)
        {
            var discussionList= context.Discussions.Where(c => c.MeetingId == meetingId).ToList();
            return discussionList;
        }

    }
}
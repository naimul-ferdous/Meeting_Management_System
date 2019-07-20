using System.Collections.Generic;
using System.Linq;
using MeetingManagementSystem.Models;
using MeetingManagementSystem.ViewModels;

namespace MeetingManagementSystem.Repositories
{
    public class MeetingLoggerRepository
    {
        MeetingManagementDbContext context = new MeetingManagementDbContext();
        //public int Insert(MeetingVM meetingVM)
        public int Insert(MeetingLoggerVM meetingVM)
        {
            var meeting = context.Meetings.FirstOrDefault(c => c.MeetingId == meetingVM.MeetingId);
            MeetingLogger meetingLogger=new MeetingLogger();
            if (meeting != null)
            {
                meetingLogger.MeetingId = meeting.MeetingId;
                //meetingLogger.MeetingId = context.Meetings.Max(c=>c.MeetingId)+1;
                meetingLogger.VenueId = meetingVM.VenueId;
                meetingLogger.BeginningTime = meeting.BeginningTime;
                meetingLogger.EndTime = meeting.EndTime;
                meetingLogger.Status = "Assigned";

            }

            
            

            context.MeetingLoggers.Add(meetingLogger);
            return context.SaveChanges();
        }
        public  int Edit(MeetingLogger meeting)
        {
            MeetingLogger _meeting = context.MeetingLoggers.FirstOrDefault(c => c.MeetingLoggerId == meeting.MeetingLoggerId);
            _meeting.VenueId = meeting.VenueId;
            _meeting.BeginningTime = meeting.BeginningTime;
            _meeting.EndTime = meeting.EndTime;
            _meeting.Status = meeting.Status;
            context.Entry(meeting).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges();
        }
        //public int Delete(int id)
        //{
        //    Meeting meeting = context.Meetings.Find(id);
        //    context.Meetings.Remove(meeting);
        //    return context.SaveChanges();
        //}
        public MeetingLogger Get(int id)
        {
            return context.MeetingLoggers.Find(id);
        }
        public List<MeetingLogger> GetAll()
        {
            return context.MeetingLoggers.ToList();
        }

        
    }
}
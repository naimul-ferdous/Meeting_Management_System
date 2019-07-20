using System.Collections.Generic;
using System.Linq;
using MeetingManagementSystem.Models;
using MeetingManagementSystem.ViewModels;
using System;

namespace MeetingManagementSystem.Repositories
{
    public class MeetingRepository
    {
        MeetingManagementDbContext context = new MeetingManagementDbContext();
        MeetingMemberRepository meetingMemberRepository = new MeetingMemberRepository();
        public int Insert(MeetingVM meeting)
        {
            Meeting meting = new Meeting();
            meting.MeetingName = meeting.MeetingName;
            meting.BeginningTime = meeting.BeginningDate.Date + meeting.BeginningTime.TimeOfDay;
            meting.EndTime = meeting.EndDate.Date + meeting.EndTime.TimeOfDay;
            meting.MeetingDescription = meeting.MeetingDescription;
            meting.Status = meeting.Status;
            meting.EmployeeId = meeting.EmployeeId;
            meting.VenueId = meeting.VenueId;
            meting.Approval = false;
            meting.CreatedById = meeting.CreatedById;
            meting.CreatedDate = meeting.CreatedDate;
            context.Meetings.Add(meting);
            int result= context.SaveChanges();
            if (result > 0)
            {
                MeetingMember member = new MeetingMember();
                member.CreatedById = meting.CreatedById;
                member.CreatedDate = DateTime.Now;
                //member.MeetingId = context.Meetings.Max(c => c.MeetingId);
                member.MeetingId = meting.MeetingId;
                member.EmployeeId = meting.EmployeeId;
                member.BeginningTime = meting.BeginningTime;
                member.EndTime = meting.EndTime;
                result = meetingMemberRepository.Insert(member);
            }
            return result;
        }
        public  int Edit(MeetingVM meeting, int? presentMeetingCallerId)
        {
            Meeting meting = context.Meetings.Find(meeting.MeetingId);
            meting.MeetingName = meeting.MeetingName;
            meting.BeginningTime = meeting.BeginningDate.Date + meeting.BeginningTime.TimeOfDay;
            meting.EndTime = meeting.EndDate.Date + meeting.EndTime.TimeOfDay;
            meting.MeetingDescription = meeting.MeetingDescription;
            meting.Status = meeting.Status;
            meting.EmployeeId = meeting.EmployeeId;
            meting.VenueId = meeting.VenueId;
            meting.Approval = meeting.Approval;
            meting.IsDeleted = meeting.IsDeleted;
            meting.ModifiedById = meeting.ModifiedById;
            meting.ModifiedDate = meeting.ModifiedDate;
            context.Entry(meting).State = System.Data.Entity.EntityState.Modified;
            int result = context.SaveChanges();

            if (result > 0 && presentMeetingCallerId != null)
            {
                int employeeId =(int) presentMeetingCallerId;
                int userEmployeeId = (int)meting.ModifiedById;
                meetingMemberRepository.Delete(meeting.MeetingId, employeeId, userEmployeeId);
            }

            if (result > 0 && meeting.EmployeeId > 0)
            {
                var meetingMember = meetingMemberRepository.Get(meeting.MeetingId, meeting.EmployeeId);
                if (meetingMember == null)
                {
                    MeetingMember member = new MeetingMember();
                    member.CreatedById = (int)meting.ModifiedById;
                    member.CreatedDate = DateTime.Now;
                    member.MeetingId = meting.MeetingId;
                    member.EmployeeId = meting.EmployeeId;
                    member.BeginningTime = meting.BeginningTime;
                    member.EndTime = meting.EndTime;
                    result = meetingMemberRepository.Insert(member);
                }
                else
                {
                    meetingMember.ModifiedById = (int)meting.ModifiedById;
                    meetingMember.ModifiedDate = DateTime.Now;
                    //meetingMember.BeginningTime = meting.BeginningTime;
                    //meetingMember.EndTime = meting.EndTime;
                    meetingMember.IsDeleted = false;
                    result = meetingMemberRepository.Edit(meetingMember);
                }
            }
            
            return result;
        }
        public int Delete(int id)
        {
            Meeting meeting = context.Meetings.Find(id);
            if (meeting != null)
            {
                meeting.IsDeleted = true;
                context.Entry(meeting).State = System.Data.Entity.EntityState.Modified;
            }
            return context.SaveChanges();
        }
        public Meeting Get(int id)
        {
            return context.Meetings.FirstOrDefault(c => c.MeetingId == id);
        }
        public List<Meeting> GetAll()
        {
             return context.Meetings.ToList();
        }
        public List<Meeting> GetMeetingOfThisWeek()
        {
            DateTime dtNow = DateTime.Now;
            // today's day of the week
            DayOfWeek dow = dtNow.DayOfWeek;
            int d = (int)dow;
            int ed = 6 - d;
            // subtract number of days from today's date
            DateTime dtFirst = dtNow.AddDays(d * -1);
            DateTime dtlast = dtNow.AddDays(ed);
            List<Meeting> meetings = context.Meetings.Where(m => m.BeginningTime >= dtFirst && m.EndTime <= dtlast).ToList();
            return meetings;
        }
        public List<Meeting> GetMeetingOfNextWeek()
        {
            DateTime dtNow = DateTime.Now;
            // today's day of the week
            DayOfWeek dow = dtNow.DayOfWeek;
            int d = (int)dow;
            int ed = 6 - d;
            // subtract number of days from today's date
            DateTime dtFirst = dtNow.AddDays(d * -1);
            DateTime dtlast = dtNow.AddDays(ed);
            DateTime nxtweekfirstday = dtlast.AddDays(1);
            DateTime nxtweeklastday = nxtweekfirstday.AddDays(6);
            List<Meeting> meetings = context.Meetings.Where(m => m.BeginningTime >= nxtweekfirstday && m.EndTime <= nxtweeklastday).ToList();
            return meetings;
        }
    }
}
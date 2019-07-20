using System;
using System.Collections.Generic;
using System.Linq;
using MeetingManagementSystem.Models;
using System.Web;
using System.IO;

namespace MeetingManagementSystem.Repositories
{
    public class MeetingMemberRepository
    {

        MeetingManagementDbContext context = new MeetingManagementDbContext();
        public int Insert(MeetingMember meetingMember)
        {
            MeetingMember _meetingMember = context.MeetingMembers.FirstOrDefault(m => m.EmployeeId == meetingMember.EmployeeId && m.MeetingId == meetingMember.MeetingId);
            if (_meetingMember != null)
            {

                _meetingMember.ModifiedById = meetingMember.CreatedById;
                _meetingMember.ModifiedDate = DateTime.Now;
                _meetingMember.BeginningTime = meetingMember.BeginningTime;
                _meetingMember.EndTime = meetingMember.EndTime;
                _meetingMember.IsDeleted = false;
                context.Entry(_meetingMember).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                context.MeetingMembers.Add(meetingMember);
            }
            
            return context.SaveChanges();
        }
        public int Edit(MeetingMember meetingMember)
        {
            context.Entry(meetingMember).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges();
        }
        public int Delete(int meetingId, int employeeId, int userEmployeeId)
        {
            MeetingMember meetingMember = context.MeetingMembers.FirstOrDefault(m => m.EmployeeId == employeeId && m.MeetingId == meetingId);
            if (meetingMember != null)
            {

                meetingMember.ModifiedById = userEmployeeId;
                meetingMember.ModifiedDate = DateTime.Now;
                meetingMember.IsDeleted = true;
                context.Entry(meetingMember).State = System.Data.Entity.EntityState.Modified;
            }
            //context.MeetingMembers.Remove(meetingMember);
            return context.SaveChanges();
        }
        public MeetingMember Get(int meetingId, int employeeId)
        {
            var member = context.MeetingMembers.SingleOrDefault(m => m.EmployeeId == employeeId && m.MeetingId == meetingId && m.IsDeleted!=true);
            return member;
        }
        public List<MeetingMember> GetAll()
        {
            return context.MeetingMembers.ToList();
        }
        public List<Meeting> GetAllMeetingsByEmployeeId(int employeeId)
        {
            return context.MeetingMembers.Where(c => c.EmployeeId == employeeId && c.IsDeleted == false).Select(c => c.Meeting).ToList();
        }
        public int MeetingFileUpload(string FileName, string FilePath, int MeetingId)
        {
            MeetingFileUpload m = new MeetingFileUpload();
            m.FileName = FileName;
            m.FilePath = FilePath;
            m.MeetingId = MeetingId;
            context.MeetingFileUploads.Add(m);
            return context.SaveChanges();
        }
        public List<MeetingFileUpload> GetFilesByMeetingId(int id)
        {
            return context.MeetingFileUploads.Where(m => m.Meeting.MeetingId == id).ToList();
        }
        public List<MeetingFileUpload> GetAllFile()
        {
            return context.MeetingFileUploads.ToList();
        }
        public List<MeetingMember> GetMembersByMeetingId(int meetingId)
        {
            return context.MeetingMembers.Where(m => m.Meeting.MeetingId == meetingId && m.IsDeleted==false).ToList();
        }
        public string GetFilePath(int id)
        {
            return context.MeetingFileUploads.Where(c => c.MeetingFileUploadId == id).Select(c => c.FilePath).FirstOrDefault();
        }

        public int DeleteFile(int id)
        {
            var meetingFile = context.MeetingFileUploads.FirstOrDefault(c => c.MeetingFileUploadId == id);
            if (meetingFile != null)
                meetingFile.IsDeleted = true;
            context.Entry(meetingFile).State = System.Data.Entity.EntityState.Modified;
            //context.MeetingFileUploads.Remove(meetingFile);
            return context.SaveChanges();
        }
        public int RestoreFile(int id)
        {
            var meetingFile = context.MeetingFileUploads.FirstOrDefault(c => c.MeetingFileUploadId == id);
            if (meetingFile != null)
                meetingFile.IsDeleted = false;
            context.Entry(meetingFile).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges();
        }
        public List<MeetingFileUpload> GetAllFileByMeetingId(int meetingId)
        {
            return context.MeetingFileUploads.Where(c => c.MeetingId == meetingId).ToList();
        }
        public List<Meeting> GetMeetingOfThisWeek(int employeeId)
        {
            DateTime dtNow = DateTime.Now;
            // today's day of the week
            DayOfWeek dow = dtNow.DayOfWeek;
            int d = (int)dow;
            int ed = 6 - d;
            // subtract number of days from today's date
            DateTime dtFirst = dtNow.AddDays(d * -1);
            DateTime dtlast = dtNow.AddDays(ed);
            List<Meeting> meetings = context.MeetingMembers.Where(m => m.BeginningTime >= dtFirst && m.EndTime <= dtlast && m.EmployeeId==employeeId && m.IsDeleted == false).Select(c => c.Meeting).ToList();
            return meetings;
        }
        public List<Meeting> GetMeetingOfNextWeek(int employeeId)
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
            List<Meeting> meetings = context.MeetingMembers.Where(m => m.BeginningTime >= nxtweekfirstday && m.EndTime <= nxtweeklastday && m.EmployeeId == employeeId && m.IsDeleted == false).Select(c=>c.Meeting).ToList();
            return meetings;
        }
    }
}
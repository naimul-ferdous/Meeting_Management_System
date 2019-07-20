using System;
using System.Collections.Generic;
using System.Linq;
using MeetingManagementSystem.Models;
using System.Data.Entity;

namespace MeetingManagementSystem.Repositories
{
    public class MeetingResultRepository
    {
        MeetingManagementDbContext context = new MeetingManagementDbContext();
        public int Insert(MeetingResult meetingResult)
        {
            context.MeetingResults.Add(meetingResult);
            return context.SaveChanges();
        }
        public int Edit(MeetingResult meetingResult)
        {
            var result = context.MeetingResults.FirstOrDefault(c => c.MeetingResultId == meetingResult.MeetingResultId);

            if (result != null)
            {
                context.MeetingResults.Attach(result);
                result.ModifiedDate = meetingResult.ModifiedDate;
                result.ModifiedById = meetingResult.ModifiedById;
                result.EmployeeId = meetingResult.EmployeeId;
                result.Announcement = meetingResult.Announcement;
                result.Result = meetingResult.Result;
                result.Status = meetingResult.Status;
            }

           // context.Entry(meetingResult).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges();
        }

        //public int Delete(int meetingResultId)
        //{
        //    MeetingResult meetingResult = new MeetingResult() {MeetingResultId = meetingResultId};
        //    context.Entry(meetingResult).State = EntityState.Deleted;
        //    return context.SaveChanges();
        //}
        public int Delete(int id)
        {
            context.MeetingResults.Remove(context.MeetingResults.Find(id));
            return context.SaveChanges();
        }
        public MeetingResult Get(int id)
        {
            return context.MeetingResults.SingleOrDefault(rd => rd.MeetingResultId == id);
        }
        public List<MeetingResult> GetAll()
        {
            return context.MeetingResults.ToList();
        }
    }
}
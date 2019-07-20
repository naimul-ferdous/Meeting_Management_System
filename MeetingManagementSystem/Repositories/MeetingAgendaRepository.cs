using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MeetingManagementSystem.Models;

namespace MeetingManagementSystem.Repositories
{
    public class MeetingAgendaRepository
    {
        MeetingManagementDbContext context = new MeetingManagementDbContext();
        public int Insert(MeetingAgenda meetingAgenda)
        {
            context.MeetingAgendas.Add(meetingAgenda);
            return context.SaveChanges();
        }
        public MeetingAgenda Get(int id)
        {
            return context.MeetingAgendas.Find(id);
        }
        public List<MeetingAgenda> GetAll()
        {
            return context.MeetingAgendas.ToList();
        }
        public List<MeetingAgenda> GetByMeetingId(int id)
        {
            return context.MeetingAgendas.Where(m=>m.MeetingId==id).ToList();
        }
        public int Edit(MeetingAgenda meetingAgenda)
        {
            var agenda = context.MeetingAgendas.FirstOrDefault(c => c.MeetingAgendaId == meetingAgenda.MeetingAgendaId);

            context.MeetingAgendas.Attach(agenda);
            agenda.ModifiedDate = meetingAgenda.ModifiedDate;
            agenda.ModifiedById = meetingAgenda.ModifiedById;
            agenda.EmployeeId = meetingAgenda.EmployeeId;
            agenda.MeetingAgendaName= meetingAgenda.MeetingAgendaName;
            //context.Entry(meetingAgenda).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges();
        }
        public int Delete(int id)
        {
            context.MeetingAgendas.Remove(context.MeetingAgendas.Find(id));
            return context.SaveChanges();
        }
    }
}
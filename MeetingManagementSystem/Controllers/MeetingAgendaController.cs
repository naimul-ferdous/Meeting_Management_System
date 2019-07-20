using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetingManagementSystem.Models;
using MeetingManagementSystem.Repositories;
using MeetingManagementSystem.ViewModels;

namespace MeetingManagementSystem.Controllers
{
    public class MeetingAgendaController : Controller
    {
        // GET: MeetingAgenda
        MeetingAgendaRepository MeetingAgendaRepository = new MeetingAgendaRepository();
        public ActionResult Index(int id)
        {
            
            var list = MeetingAgendaRepository.GetByMeetingId(id);
            var list2 = (from meetingAgenda in list select new
            {
                meetingAgenda.MeetingAgendaId,
                meetingAgenda.MeetingAgendaName,
                meetingAgenda.EmployeeId,
                meetingAgenda.Employee.EmployeeName,
                meetingAgenda.MeetingId
            }).ToList();
            return Json(list2, JsonRequestBehavior.AllowGet);
        }

        EmployeeRepository employeeRepository = new EmployeeRepository();
        private Employee GetEmployee()
        {
            UserSessionVM userSession = (UserSessionVM)Session["UserSession"];
            var employee = employeeRepository.GetAll().FirstOrDefault(c => c.Email == userSession.User.EmailId);
            return employee;
        }
        [HttpPost]
        public ActionResult Create(MeetingAgenda meetingAgenda)
        {
            meetingAgenda.CreatedById = GetEmployee().EmployeeId;
            meetingAgenda.CreatedDate = DateTime.Now;
            return Json(MeetingAgendaRepository.Insert(meetingAgenda), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(MeetingAgenda meetingAgenda)
        {
            meetingAgenda.ModifiedById = GetEmployee().EmployeeId;
            meetingAgenda.ModifiedDate = DateTime.Now;
            return Json(MeetingAgendaRepository.Edit(meetingAgenda), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int id)
        {
            return Json(MeetingAgendaRepository.Delete(id), JsonRequestBehavior.AllowGet);
        }
    }
}
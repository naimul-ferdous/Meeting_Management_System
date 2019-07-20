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
    public class MeetingResultController : Controller
    {
        MeetingResultRepository repository = new MeetingResultRepository();
        MeetingManagementDbContext context = new MeetingManagementDbContext();
        // GET: MeetingResult
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllMeetingResult(int meetingResultId)
        {
            var dataList = (from meetingResult in context.MeetingResults
                            join emp in context.Employees on meetingResult.EmployeeId equals emp.EmployeeId
                            join meeting in context.Meetings on meetingResult.MeetingId equals meeting.MeetingId
                            where meetingResult.MeetingId == meetingResultId
                            select new
                            {
                                meetingResult.MeetingResultId,
                                Announcement = meetingResult.Announcement,
                                meetingResult.Result,
                                meetingResult.Status,
                                meetingResult.EmployeeId,
                                emp.EmployeeName,
                                meetingResult.MeetingId,
                                meeting.MeetingName,
                            }).ToList();

            return Json(dataList, JsonRequestBehavior.AllowGet);
        }

        EmployeeRepository employeeRepository = new EmployeeRepository();
        private Employee GetEmployee()
        {
            UserSessionVM userSession = (UserSessionVM)Session["UserSession"];
            var employee = employeeRepository.GetAll().FirstOrDefault(c => c.Email == userSession.User.EmailId);
            return employee;
        }
        public ActionResult Create(MeetingResult meetingResult)
        {
            meetingResult.CreatedById = GetEmployee().EmployeeId;
            meetingResult.CreatedDate = DateTime.Now;
            int rowAff = repository.Insert(meetingResult);
            return Json(rowAff, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(MeetingResult meetingResult)
        {
            meetingResult.ModifiedById= GetEmployee().EmployeeId;
            meetingResult.ModifiedDate = DateTime.Now;
            int row = repository.Edit(meetingResult);
            //if (row > 0)
            //{
            //    var list = (from meeting in context.Meetings
            //        select new
            //        {
            //            meeting.MeetingId,
            //            meeting.MeetingName
            //        }).ToList();
            //    return Json(list, JsonRequestBehavior.AllowGet);
            //}
            return Json(row, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int meetingResultId)
        {
            MeetingResult meetingResult = new MeetingResult() { MeetingResultId = meetingResultId };

            int row = repository.Delete(meetingResultId);
            return Json(row, JsonRequestBehavior.AllowGet);
        }



        public ActionResult GetEmployeeInfo()
        {
            var list = (from emp in context.Employees
                        select new
                        {
                            emp.EmployeeId,
                            emp.EmployeeName
                        }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetMeetingInfo()
        {
            var list = (from meeting in context.Meetings
                        select new
                        {
                            meeting.MeetingId,
                            meeting.MeetingName
                        }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
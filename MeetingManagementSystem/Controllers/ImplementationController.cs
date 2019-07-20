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
    public class ImplementationController : Controller
    {
        ImplementationRepository implementationRepository = new ImplementationRepository();

        MeetingRepository meetingRepository=new MeetingRepository();
        EmployeeRepository employeeRepository=new EmployeeRepository();
        private Employee GetEmployee()
        {
            UserSessionVM userSession = (UserSessionVM)Session["UserSession"];
            var employee = employeeRepository.GetAll().FirstOrDefault(c => c.Email == userSession.User.EmailId);
            return employee;
        }
        public ActionResult Implementation()
        {
            return View();
        }
        public ActionResult GetMeetingInfo()
        {
            var meetings = meetingRepository.GetAll();
            var list = (from mt in meetings
                        select new
                        {
                            mt.MeetingId,
                            mt.MeetingName
                        }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult Create(Implementation implementation)
        {
            implementation.CreatedById = GetEmployee().EmployeeId;
            implementation.CreatedDate = DateTime.Now;
            int row = implementationRepository.Insert(implementation);
            return Json(row, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(Implementation implementation)
        {

            implementation.ModifiedById = GetEmployee().EmployeeId;
            implementation.ModifiedDate = DateTime.Now;
            int row = implementationRepository.Edit(implementation);
            return Json(row, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int implementationId)
        {
            int row = implementationRepository.Delete(implementationId);
            return Json(row, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(int id)
        {
            var implementations = implementationRepository.GetAllByMeetingId(id);
            var meetings = meetingRepository.GetAll();
            var employees = employeeRepository.GetAll();

            var dataList = (from imp in implementations
                            join mt in meetings on imp.MeetingId equals mt.MeetingId
                            join emp in employees on imp.EmployeeId equals emp.EmployeeId
                            select new
                            {
                                imp.ImplementationId,
                                imp.ImplementationDescription,
                                imp.MeetingId,
                                mt.MeetingName,
                                imp.EmployeeId,
                                emp.EmployeeName

                            }).ToList();

            return Json(dataList, JsonRequestBehavior.AllowGet);
        }
    }
}
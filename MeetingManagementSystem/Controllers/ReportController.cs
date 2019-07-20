using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetingManagementSystem.Repositories;

namespace MeetingManagementSystem.Controllers
{
    public class ReportController : Controller
    {
        VenueRepository venueRepository = new VenueRepository();
        EmployeeRepository employeeRepository = new EmployeeRepository();
        // GET: Report
        public ActionResult MeetingList()
        {
            return View();
        }

        public ActionResult GetVenueInfo()
        {
            var allVenues = venueRepository.GetAll();
            var list = (from venue in allVenues
                select new
                {
                    venue.VenueId,
                    venue.VenueName
                }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEmployeeInfo()
        {
            var allEmployees = employeeRepository.GetAll();
            var list = (from emp in allEmployees
                select new
                {
                    emp.EmployeeId,
                    emp.EmployeeName
                }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DateWiseMeeting()
        {
            return View();
        }

    }

}
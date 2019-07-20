using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetingManagementSystem.Hubs;
using MeetingManagementSystem.Models;
using MeetingManagementSystem.Repositories;
using MeetingManagementSystem.ViewModels;

namespace MeetingManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        MeetingRepository meetingRepository = new MeetingRepository();
        public ActionResult Index()
        {
           
            //ViewBag.TodayMeetings = meetingRepository.GetAll().Count(c => c.BeginningTime.Date==DateTime.Today);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        EmployeeRepository employeeRepository = new EmployeeRepository();
        MeetingMemberRepository meetingMemberRepository = new MeetingMemberRepository();
        private Employee GetEmployee()
        {
            UserSessionVM userSession = (UserSessionVM)Session["UserSession"];
            var employee = employeeRepository.GetAll().FirstOrDefault(c => c.Email == userSession.User.EmailId);
            return employee;
        }

        public List<Meeting> AllMeetings()
        {
            var employee = GetEmployee();
            List<Meeting> allMeetings;

            if (employee != null)
            {
                var employeeCalledMeetings = new HashSet<int>(meetingRepository.GetAll()
                    .Where(c => c.EmployeeId == employee.EmployeeId && c.IsDeleted == false)
                    .Select(c => c.MeetingId)).ToList();
                allMeetings = meetingRepository.GetAll().Where(c => c.EmployeeId == employee.EmployeeId && c.IsDeleted == false)
                    .ToList();
                var meetingMembers = meetingMemberRepository.GetAllMeetingsByEmployeeId(employee.EmployeeId)
                    .Where(x => !employeeCalledMeetings.Contains(x.MeetingId)).ToList();
                foreach (var item in meetingMembers)
                {
                    allMeetings.Add(item);
                }
            }
            else
            {
                allMeetings = meetingRepository.GetAll().ToList();
            }

            return allMeetings;
        }
        public List<Meeting> ThisWeekMeetings()
        {
            var employee = GetEmployee();
            List<Meeting> allMeetings;

            if (employee != null)
            {
                var employeeCalledMeetings = new HashSet<int>(meetingRepository.GetMeetingOfThisWeek()
                    .Where(c => c.EmployeeId == employee.EmployeeId && c.IsDeleted == false)
                    .Select(c => c.MeetingId)).ToList();
                allMeetings = meetingRepository.GetMeetingOfThisWeek().Where(c => c.EmployeeId == employee.EmployeeId && c.IsDeleted == false)
                    .ToList();
                var meetingMembers = meetingMemberRepository.GetMeetingOfThisWeek(employee.EmployeeId).Where(x => !employeeCalledMeetings.Contains(x.MeetingId)).ToList();
                foreach (var item in meetingMembers)
                {
                    allMeetings.Add(item);
                }
            }
            else
            {
                allMeetings = meetingRepository.GetAll().ToList();
            }

            return allMeetings;
        }
        public List<Meeting> NextWeekMeetings()
        {
            var employee = GetEmployee();
            List<Meeting> allMeetings;

            if (employee != null)
            {
                var employeeCalledMeetings = new HashSet<int>(meetingRepository.GetMeetingOfNextWeek()
                    .Where(c => c.EmployeeId == employee.EmployeeId && c.IsDeleted == false)
                    .Select(c => c.MeetingId)).ToList();
                allMeetings = meetingRepository.GetMeetingOfNextWeek().Where(c => c.EmployeeId == employee.EmployeeId && c.IsDeleted == false)
                    .ToList();
                var meetingMembers = meetingMemberRepository.GetMeetingOfNextWeek(employee.EmployeeId).Where(x => !employeeCalledMeetings.Contains(x.MeetingId)).ToList();
                foreach (var item in meetingMembers)
                {
                    allMeetings.Add(item);
                }
            }
            else
            {
                allMeetings = meetingRepository.GetAll().ToList();
            }

            return allMeetings;
        }
        public ActionResult Dashboard()
        {
            ViewBag.TotalMeetings = AllMeetings().Count();
            //ViewBag.TodayMeetings = meetingRepository.GetAll().Count(c => c.BeginningTime.Date==DateTime.Today);
            ViewBag.ThisWeekMeetings = ThisWeekMeetings().Count();
            ViewBag.NextWeekMeetings = NextWeekMeetings().Count();
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        MeetingManagementDbContext db = new MeetingManagementDbContext();
        public JsonResult Get()
        {

            //using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MeetingManagementDbContext"].ConnectionString))
            //{

            //    //connection.Open();
            //    //using (SqlCommand command = new SqlCommand(@"SELECT [MeetingId],[EmployeeId] FROM [dbo].[MeetingMembers] member JOIN [dbo].[Employees] emp ON member.EmployeeId = emp.EmployeeId  ", connection))
            //    using (SqlCommand command = new SqlCommand(@"SELECT * FROM [dbo].[MeetingMembers] ", connection))
            //    {
            //        //SqlCommand command = new SqlCommand(@"SELECT * FROM [dbo].[MeetingMembers] ", connection))

            //        // Make sure the command object does not already have
            //        // a notification object associated with it.
            //        command.Notification = null;

            //        SqlDependency dependency = new SqlDependency(command);
            //        dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

            //        if (connection.State == ConnectionState.Closed)
            //            connection.Open();

            //        SqlDataReader reader = command.ExecuteReader();

            //        var listOfMeetings = reader.Cast<IDataRecord>()
            //            .Select(x => new
            //            {
            //                //Id = (int)x["Id"],
            //                MeetingId = (int)x["MeetingId"],
            //                EmployeeId = (int)x["EmployeeId"],
            //                //EmployeeName = (string)x["EmployeeName"],
            //            }).ToList();

            //        return Json(listOfMeetings, JsonRequestBehavior.AllowGet);


            //        //var totalMeetings = listOfMeetings.Count();
            //        //return Json(totalMeetings, JsonRequestBehavior.AllowGet);

            //    }
            //}
            return Json(null);
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            MeetingManagementHub.Show();
        }

        public PartialViewResult GetMenuList()
        {
            try
            {
                var result = db.SecResources.ToList();
                return PartialView("MenuPartial", result);
            }
            catch (Exception ex)
            {
                var error = ex.Message.ToString();
                return PartialView("Error");
            }
        }
    }
}
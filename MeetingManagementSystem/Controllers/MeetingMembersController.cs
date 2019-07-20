using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using MeetingManagementSystem.Models;
using MeetingManagementSystem.Repositories;
using MeetingManagementSystem.ViewModels;
using Dapper;
using System.IO;


namespace MeetingManagementSystem.Controllers
{
    public class MeetingMembersController : Controller
    {
        //MeetingManagementDbContext context = new MeetingManagementDbContext();
        MeetingRepository meetingRepository = new MeetingRepository();
        EmployeeRepository employeeRepository = new EmployeeRepository();
        MeetingMemberRepository meetingMemberRepository = new MeetingMemberRepository();
        DiscussionRepository discussionRepository = new DiscussionRepository();
        
        // GET: MeetingMembers
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult AddMembers(int meetingId)
        {
            //List<MeetingMemberVM> meetingMembers = new List<MeetingMemberVM>();

            //var meetings = meetingRepository.GetAll();
            //var meeting = meetings.FirstOrDefault(c => c.MeetingId == meetingId);
            //var meetingStart = meeting.BeginningTime;
            //var meetingEnd = meeting.EndTime;

            //var meetingMemberLog = meetingMembeRepository.GetAll();

            //var busyEmployeeList = meetingMemberLog.Where(c => c.BeginningTime < meetingEnd && meetingStart < c.EndTime).GroupBy(c => c.EmployeeId).Select(c => c.FirstOrDefault()).Select(c => c.Employee).ToList();
            //var allEmployees = employeeRepository.GetAll();
            //IEnumerable<Employee> availableEmployeeList;
            //if (busyEmployeeList.Count > 0)
            //{
            //    availableEmployeeList = allEmployees.Except(busyEmployeeList).ToList();

            //}
            //else
            //{
            //    availableEmployeeList = allEmployees;
            //}


            //foreach (var item in availableEmployeeList)
            //{
            //    MeetingMemberVM meetingMember = new MeetingMemberVM();
            //    meetingMember.MeetingId = (int)meetingId;
            //    meetingMember.EmployeeId = item.EmployeeId;
            //    meetingMember.Employee = employeeRepository.Get(item.EmployeeId);
            //    meetingMember.Department = meetingMember.Employee.Department;
            //    meetingMember.Designation = meetingMember.Employee.Designation;
            //    meetingMembers.Add(meetingMember);
            //}

            //var list = (from employee in availableEmployeeList
            //            select new
            //            {
            //                employee.EmployeeName,
            //                employee.Department,
            //                employee.Designation
            //            }).ToList();

            var meetingMembers= AvailableEmployees(meetingId);
            return View(meetingMembers);
        }
       
        private IEnumerable<MeetingMemberVM> AvailableEmployees(int meetingId)
        {
            List<MeetingMemberVM> meetingMembers = new List<MeetingMemberVM>();

            var meetings = meetingRepository.GetAll();
            var meeting = meetings.FirstOrDefault(c => c.MeetingId == meetingId);
            var meetingStart = meeting.BeginningTime;
            var meetingEnd = meeting.EndTime;

            var meetingMemberLog = meetingMemberRepository.GetAll();

            var busyEmployeeList = new HashSet<int>(meetingMemberLog.Where(c => c.BeginningTime < meetingEnd && meetingStart < c.EndTime)
                .GroupBy(c => c.EmployeeId).Select(c => c.FirstOrDefault()).Select(c => c.EmployeeId)).ToList();
            var allEmployees = employeeRepository.GetAll().ToList();
            IEnumerable<Employee> availableEmployeeList;
            if (busyEmployeeList.Count > 0)
            {
                availableEmployeeList = allEmployees.Where(x => !busyEmployeeList.Contains(x.EmployeeId)).ToList();
            }
            else
            {
                availableEmployeeList = allEmployees;
            }


            foreach (var item in availableEmployeeList)
            {
                MeetingMemberVM meetingMember = new MeetingMemberVM();
                meetingMember.MeetingId = (int)meetingId;
                meetingMember.EmployeeId = item.EmployeeId;
                meetingMember.Employee = employeeRepository.Get(item.EmployeeId);
                meetingMember.Department = meetingMember.Employee.Department;
                meetingMember.Designation = meetingMember.Employee.Designation;
                meetingMembers.Add(meetingMember);
            }


            return meetingMembers;
        }
        public JsonResult Add_Send_Members(IEnumerable<MeetingMemberVM> meetingMembers)
        {
            List<MeetingMemberVM> addedMeetingMembers=new List<MeetingMemberVM>();
            foreach (var item in meetingMembers)
            {
                //if (item.IsSelected)
                //{
                    MeetingMember member = new MeetingMember();
                    member.MeetingId = item.MeetingId;
                    member.EmployeeId = item.EmployeeId;

                    var meeting = meetingRepository.Get(item.MeetingId);
                    member.BeginningTime = meeting.BeginningTime;
                    member.EndTime = meeting.EndTime;

                    meetingMemberRepository.Insert(member);
                    addedMeetingMembers.Add(item);

                //}
            }
            return Json(addedMeetingMembers,JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult AvailableEmployeesPartial(int meetingId)
        {
            var meetingMembers = AvailableEmployees(meetingId);
            return PartialView(meetingMembers);
        }
        public PartialViewResult AddMembersPartial(IEnumerable<MeetingMemberVM> meetingMembers)
        {
            List<MeetingMemberPartialVM> addedMeetingMembers = new List<MeetingMemberPartialVM>();
            if (meetingMembers != null)
            {
                foreach (var item in meetingMembers)
                {
                    //if (item.IsSelected)
                    //{
                        MeetingMember member = new MeetingMember();
                        member.MeetingId = item.MeetingId;
                        member.EmployeeId = item.EmployeeId;
                        var meeting = meetingRepository.Get(item.MeetingId);
                        member.BeginningTime = meeting.BeginningTime;
                        member.EndTime = meeting.EndTime;

                        meetingMemberRepository.Insert(member);


                        MeetingMemberPartialVM partialMember = new MeetingMemberPartialVM();
                        var employee = employeeRepository.Get(member.EmployeeId);
                        partialMember.MeetingId = item.MeetingId;
                        partialMember.EmployeeId = member.EmployeeId;
                        partialMember.EmployeeName = employee.EmployeeName;
                        partialMember.Department = employee.Department.DepartmentName;
                        partialMember.Designation = employee.Designation.DesignationName;

                        addedMeetingMembers.Add(partialMember);

                    //}
                }
            }
            
            return PartialView(addedMeetingMembers);
        }
        [HttpPost]
        public ActionResult AddMembers(IEnumerable<MeetingMemberVM> meetingMembers)
        {
            foreach (var item in meetingMembers)
            {
                //if (item.IsSelected)
                //{
                    MeetingMember member = new MeetingMember();
                    member.MeetingId = item.MeetingId;
                    member.EmployeeId = item.EmployeeId;

                    var meeting = meetingRepository.Get(item.MeetingId);
                    member.BeginningTime = meeting.BeginningTime;
                    member.EndTime = meeting.EndTime;

                    meetingMemberRepository.Insert(member);


                //}
            }
           // context.SaveChanges();

            return RedirectToAction("Meeting", "Meeting");
        }

        public ActionResult ShowMembers(int meetingId)
        {
            List<MeetingMemberVM> meetingMembers = new List<MeetingMemberVM>();

            var member = meetingMemberRepository.GetMembersByMeetingId(meetingId).ToList();
            

            foreach (var item in member)
            {
                MeetingMemberVM meetingMember = new MeetingMemberVM();
                meetingMember.MeetingId = (int)meetingId;
                meetingMember.EmployeeId = item.EmployeeId;
                meetingMember.Employee = employeeRepository.Get(item.EmployeeId);
                meetingMember.Department = meetingMember.Employee.Department;
                meetingMember.Designation = meetingMember.Employee.Designation;
                meetingMembers.Add(meetingMember);
            }
            ShowMeetingDetailsVM sm = new ShowMeetingDetailsVM();
            sm.Meeting = meetingRepository.Get(meetingId);
            sm.Employees = employeeRepository.GetAll();
            //sm.meetingMembers = meetingMembers;
            sm.AvailableEmployees = AvailableEmployees(meetingId);
            sm.meetingFileUploads = meetingMemberRepository.GetFilesByMeetingId(meetingId);
            sm.Discussions= discussionRepository.GetAllByMeetingId(meetingId).ToList();
            //return View(meetingMembers);            
            return View(sm);
        }
        private Employee GetEmployee()
        {
            UserSessionVM userSession = (UserSessionVM)Session["UserSession"];
            var employee = employeeRepository.GetAll().FirstOrDefault(c => c.Email == userSession.User.EmailId);
            return employee;
        }
        public ActionResult RemoveMember(int _meetingId, int employeeId)
        {
            var userEmployeeId = GetEmployee().EmployeeId;
           int row= meetingMemberRepository.Delete(_meetingId, employeeId, userEmployeeId);
           return RedirectToAction("MeetingDetails", "Meeting", new { meetingId = _meetingId });
        }


        private string senderEmailAddress = "user@gmail.com";
        private string password = "userPassword";

        public ActionResult SendingEmailMembers(int _meetingId)
        {
            var meetingMembers = meetingMemberRepository.GetMembersByMeetingId(_meetingId).ToList();
            var meeting = meetingRepository.Get(_meetingId); ;

            foreach (var member in meetingMembers)
            {
                SendEmail(meeting, member);
            }
            return RedirectToAction("MeetingDetails", "Meeting", new { meetingId = _meetingId });
        }

        public ActionResult SendingEmailMember(int _meetingId, int employeeId)
        {
            var member = meetingMemberRepository.Get(_meetingId , employeeId);
            var meeting = meetingRepository.Get(_meetingId);
           
            SendEmail(meeting, member);

            return RedirectToAction("MeetingDetails", "Meeting", new { meetingId = _meetingId });
        }
        public ActionResult SendNotificationToCalender(int _meetingId)
        {
            CalendarApi calendarApi = new CalendarApi();
            var meetingMembers = meetingMemberRepository.GetMembersByMeetingId(_meetingId).ToList();
            var meeting = meetingRepository.Get(_meetingId); ;
            
                calendarApi.AddEvent(meeting, meetingMembers);
            
            //return RedirectToAction("ShowMembers", new { meetingId = _meetingId });
            return RedirectToAction("MeetingDetails", "Meeting",new { meetingId = _meetingId });

        }
        private void SendEmail(Meeting meeting, MeetingMember member)
        {
            var caller = meeting.Employee.EmployeeName;
            var venue = meeting.Venue.VenueName;

            var dod = "<span><strong>Hello Mr. </strong>" + " " + member.Employee.EmployeeName + ",</span>" + "<br/>"
                      + "<span>I would like to invite you on a meeting. Meeting details are given bellow.</span>" + "<br/>"
                      + "<span> <strong>Meeting Topics </strong> :" + " " + meeting.MeetingName + "</span>" + "<br/>"
                      + "<span> <strong>Description </strong> :" + " " + meeting.MeetingDescription + "</span>" + "<br/>"
                      + "<span> <strong>Meeting Caller</strong> :" + " " + caller + "</span>" + "<br/>"
                      + "<span> <strong>Venue</strong> :" + " " + venue + "</span>" + "<br/>"
                      + "<span> <strong>Beginning Time </strong> :" + " " + meeting.BeginningTime + "</span>" + "<br/>"
                      + "<span> <strong>End Time</strong> :" + " " + meeting.EndTime + "</span>" + "<br/>"
                      + "<span>Please confirm your attendance.</span>" + "<br/>";


            var body = dod;
            var message = new MailMessage();
            message.To.Add(new MailAddress(member.Employee.Email)); // replace with valid value 
            message.From = new MailAddress(senderEmailAddress,"Kazi Shalauddin"); // replace with valid value
            message.Subject = "New meeting invitation";
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = senderEmailAddress, // replace with valid value
                    Password = password // replace with valid value
                };
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(message);
            }
        }
        [HttpPost]
        public ActionResult UploadFile(int MeetingId)
        {
            var file = Request.Files[0];
            String FileExt = Path.GetExtension(file.FileName).ToUpper();
            if (file.ContentLength > 0 && FileExt==".PDF")
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/"), fileName);
                file.SaveAs(path);
                meetingMemberRepository.MeetingFileUpload(fileName, path, MeetingId);
            }

            //return RedirectToAction("ShowMembers", new { meetingId = MeetingId });
            return RedirectToAction("MeetingDetails", "Meeting", new { meetingId = MeetingId });

        }
        //public ActionResult Upload()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Upload(MeetingFileUpload E)
        //{
        //    String FileExt = Path.GetExtension(E.Files.FileName).ToUpper();
        //    if (FileExt == ".PDF")
        //    {
        //        Byte[] data = new byte[E.Files.ContentLength];
        //        E.Files.InputStream.Read(data, 0, E.Files.ContentLength);
        //        E.FileName = E.Files.FileName; ;
        //        E.FileContent = data;
        //        if (new DataLayer().SaveFileDetails(E))
        //        {
        //            return RedirectToAction("Index", "meeting");
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index", "meeting");
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "meeting");
        //    }
        //}
        //public ActionResult ShowFile()
        //{
        //    return View(new DataLayer().GetFileList());
        //}
        [HttpGet]
        public FileResult DownLoadFile(int id)
        {
            List<MeetingFileUpload> ObjFiles = meetingMemberRepository.GetAllFile();
            var FileById = (from FC in ObjFiles
                            where FC.MeetingFileUploadId.Equals(id)
                            select new { FC.FileName, FC.FilePath }).ToList().FirstOrDefault();
           
           return File(FileById.FilePath, "application/pdf", FileById.FileName);
        }

        //[HttpPost]
        // [Authorize]
        public ActionResult DeleteFile(int id)
        {
            int row = 0;
            string filePath = meetingMemberRepository.GetFilePath(id);

            if (filePath != null || filePath != string.Empty)
            {
                if (System.IO.File.Exists(filePath))
                {
                    //System.IO.File.Delete(filePath);
                   row= meetingMemberRepository.DeleteFile(id);
                }
                //! string.IsNullOrEmpty(filePath)

            }

            return Json(row, JsonRequestBehavior.AllowGet);

        }
    }
}
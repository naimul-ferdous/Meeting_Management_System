using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetingManagementSystem.Models;
using MeetingManagementSystem.Repositories;
using MeetingManagementSystem.ViewModels;

namespace MeetingManagementSystem.Controllers
{
    [HasAuthorization]
    public class All_MeetingsController : Controller
    {

        MeetingRepository meetingRepository = new MeetingRepository();
        VenueRepository venueRepository = new VenueRepository();

        // GET: Meeting
        public ActionResult All_Meetings()
        {
            return View();
        }
        private List<Meeting> AllMeetings()
        {
            var allMeetings = meetingRepository.GetAll().ToList();
            return allMeetings;
        }
        public List<Meeting> ThisWeekMeetings()
        {
            var allMeetings = meetingRepository.GetMeetingOfThisWeek().ToList();
            return allMeetings;
        }
        public List<Meeting> NextWeekMeetings()
        {

            var allMeetings = meetingRepository.GetMeetingOfNextWeek().ToList();
            return allMeetings;
        }
        public ActionResult CurrentWeekMeeting()
        {
            //var employee = GetEmployee();
            //var list = meetingRepository.GetMeetingOfThisWeek().Where(e => e.EmployeeId == employee.EmployeeId && e.IsDeleted == false);
            var list = ThisWeekMeetings();
            var list2 = (from meeting in list
                         select new
                         {
                             meeting.MeetingId,
                             meeting.MeetingName,
                             meeting.EmployeeId,
                             meeting.Employee.EmployeeName,
                             meeting.BeginningTime,
                             meeting.EndTime,
                             meeting.VenueId,
                             VenueName = meeting.VenueId == 0 ? "No Venue Added" : meeting.Venue.VenueName,
                             meeting.MeetingDescription,
                             meeting.IsDeleted
                         }).OrderByDescending(c => c.MeetingId).ToList();
            //return View(list2);
            return Json(list2, JsonRequestBehavior.AllowGet);
        }
        public ActionResult NextWeekMeeting()
        {
            //var employee = GetEmployee();
            //var list = meetingRepository.GetMeetingOfNextWeek().Where(e => e.EmployeeId == employee.EmployeeId && e.IsDeleted == false);
            var list = NextWeekMeetings();
            var list2 = (from meeting in list
                         select new
                         {
                             meeting.MeetingId,
                             meeting.MeetingName,
                             meeting.EmployeeId,
                             meeting.Employee.EmployeeName,
                             meeting.BeginningTime,
                             meeting.EndTime,
                             meeting.VenueId,
                             VenueName = meeting.VenueId == 0 ? "No Venue Added" : meeting.Venue.VenueName,
                             meeting.MeetingDescription,
                             meeting.IsDeleted
                         }).OrderByDescending(c => c.MeetingId).ToList();

            return Json(list2, JsonRequestBehavior.AllowGet);

        }

        // GET: Meeting/Create

        public ActionResult Create()
        {
            return View();
        }

        // POST: Meeting/Create
        [HttpPost]
        [HasAuthorization(AccessLevel = 1)]
        public ActionResult Create(MeetingVM meeting)
        {
            meeting.CreatedById = GetEmployee().EmployeeId;
            meeting.CreatedDate = DateTime.Now;
            return Json(meetingRepository.Insert(meeting), JsonRequestBehavior.AllowGet);
        }

        // GET: Meeting/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
        
        // POST: Meeting/Edit/5
        [HttpPost]
        [HasAuthorization(AccessLevel = 2)]
        public ActionResult Edit(MeetingVM meeting, int? presentVenueId,int? presentMeetingCallerId)
        {
            if (meeting.VenueId == 0)
            {
                if (presentVenueId != null) meeting.VenueId = (int)presentVenueId;
            }

            
            if (meeting.EmployeeId== 0)
            {
                if (presentMeetingCallerId != null) meeting.EmployeeId = (int)presentMeetingCallerId;
                
            }
            meeting.ModifiedById = GetEmployee().EmployeeId;
            meeting.ModifiedDate = DateTime.Now;
            if (meeting.EmployeeId > 0)
            {
                return Json(meetingRepository.Edit(meeting, presentMeetingCallerId), JsonRequestBehavior.AllowGet);
            }
            return Json(meetingRepository.Edit(meeting,null), JsonRequestBehavior.AllowGet);
        }

        // GET: Meeting/Delete/5
        [HasAuthorization(AccessLevel = 3)]
        public ActionResult Delete(int id)
        {
            return Json(meetingRepository.Delete(id), JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetVenues(DateTime? beginningTime, DateTime? endTime)
        {
            var meetingStart = beginningTime;
            var meetingEnd = endTime;
            var allMeetings = meetingRepository.GetAll();

            var busyVenueList = new HashSet<int>(allMeetings.Where(c => c.BeginningTime < meetingEnd && meetingStart < c.EndTime.AddMinutes(30))
                .GroupBy(c => c.VenueId).Select(c => c.FirstOrDefault()).Select(c => c.VenueId)).ToList();
            var allVenues = venueRepository.GetAll();
            List<Venue> avilableVenueList;
            if (busyVenueList.Count > 0)
            {
                avilableVenueList = allVenues.Where(x => !busyVenueList.Contains(x.VenueId)).ToList();
            }
            else
            {
                avilableVenueList = allVenues;
            }
            var list = (from venues in avilableVenueList
                        select new
                        {
                            venues.VenueId,
                            venues.VenueName
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllEmployees(DateTime? beginningTime, DateTime? endTime)
        {
            var meetingStart = beginningTime;
            var meetingEnd = endTime;
            var meetingMemberLog = meetingMemberRepository.GetAll();

            var busyEmployeeList = new HashSet<int>(meetingMemberLog.Where(c => c.BeginningTime < meetingEnd && meetingStart < c.EndTime.AddMinutes(30) && c.IsDeleted != true)
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
            var list = (from employee in availableEmployeeList
                        select new
                        {
                            employee.EmployeeId,
                            employee.EmployeeName
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            //int n = 1;
            var allMeetings = AllMeetings();

            var dataList = (from meeting in allMeetings
                select new
                {
                    //SL=n++,
                    meeting.MeetingId,
                    meeting.MeetingName,
                    //BeginningTime=meeting.BeginningTime.ToString("dd - MMM - yyyy hh:mm tt"),
                    meeting.BeginningTime,
                    meeting.EndTime,
                    meeting.EmployeeId,
                    meeting.Employee.EmployeeName,
                    meeting.VenueId,
                    VenueName = meeting.VenueId == 0 ? "No Venue Added" : meeting.Venue.VenueName,
                    meeting.MeetingDescription,
                    meeting.Status,
                    meeting.IsDeleted
                }).OrderByDescending(c => c.MeetingId).ToList();

            return Json(dataList, JsonRequestBehavior.AllowGet);
        }

        //private List<Meeting> AllMeetings()
        //{
        //    var employee = GetEmployee();
        //    List<Meeting> allMeetings;

        //    if (employee != null)
        //    {
        //        var employeeCalledMeetings = new HashSet<int>(meetingRepository.GetAll()
        //            .Where(c => c.EmployeeId == employee.EmployeeId && c.IsDeleted == false)
        //            .Select(c => c.MeetingId)).ToList();
        //        allMeetings = meetingRepository.GetAll().Where(c => c.EmployeeId == employee.EmployeeId && c.IsDeleted == false)
        //            .ToList();
        //        var meetingMembers = meetingMemberRepository.GetAllMeetingsByEmployeeId(employee.EmployeeId)
        //            .Where(x => !employeeCalledMeetings.Contains(x.MeetingId) && x.IsDeleted == false).ToList();
        //        foreach (var item in meetingMembers)
        //        {
        //            allMeetings.Add(item);
        //        }
        //    }
        //    else
        //    {
        //        allMeetings = meetingRepository.GetAll().ToList();
        //    }

        //    return allMeetings;
        //}

        private Employee GetEmployee()
        {
            UserSessionVM userSession = (UserSessionVM)Session["UserSession"];
            var employee = employeeRepository.GetAll().FirstOrDefault(c => c.Email == userSession.User.EmailId);
            return employee;
        }


        EmployeeRepository employeeRepository = new EmployeeRepository();
        MeetingMemberRepository meetingMemberRepository = new MeetingMemberRepository();
        DiscussionRepository discussionRepository = new DiscussionRepository();
        ImplementationRepository implementationRepository = new ImplementationRepository();
        private IEnumerable<MeetingMemberVM> AvailableEmployees(int meetingId)
        {
            List<MeetingMemberVM> meetingMembers = new List<MeetingMemberVM>();

            var meetings = meetingRepository.GetAll();
            var meeting = meetings.FirstOrDefault(c => c.MeetingId == meetingId);
            var meetingStart = meeting.BeginningTime;
            var meetingEnd = meeting.EndTime;

            var meetingMemberLog = meetingMemberRepository.GetAll();

            var busyEmployeeList = new HashSet<int>(meetingMemberLog.Where(c => c.BeginningTime < meetingEnd && meetingStart < c.EndTime && c.IsDeleted != true)
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
        public ActionResult MeetingDetails(int meetingId)
        {
            List<MeetingMemberVM> meetingMembers = new List<MeetingMemberVM>();

            var member = meetingMemberRepository.GetMembersByMeetingId(meetingId).OrderBy(c => c.MeetingMemberId).ToList();
            List<MeetingMemberVM> internalMembers = new List<MeetingMemberVM>();
            List<MeetingMemberVM> externalMembers = new List<MeetingMemberVM>();

            var meetingMemberLog = meetingMemberRepository.GetAll();
            foreach (var item in member)
            {
                MeetingMemberVM meetingMember = new MeetingMemberVM();
                meetingMember.MeetingId = (int)meetingId;
                meetingMember.EmployeeId = item.EmployeeId;
                meetingMember.Employee = employeeRepository.Get(item.EmployeeId);
                meetingMember.Department = meetingMember.Employee.Department;
                meetingMember.Designation = meetingMember.Employee.Designation;
                meetingMember.BeginningTime = item.BeginningTime;
                meetingMember.EndTime = item.EndTime;
              
                var employeeBusyMeetingList = new HashSet<int>(
                    meetingMemberLog.Where(c => c.MeetingId != meetingId && c.EmployeeId==item.EmployeeId &&c.BeginningTime < item.EndTime && item.BeginningTime < c.EndTime.AddMinutes(30) && c.IsDeleted != true).Select(c=>c.MeetingId).ToList());
                //meetingMembers.Add(meetingMember);
                meetingMember.IsBusy = employeeBusyMeetingList.Any();
                if (meetingMember.Employee.EmployeeTypeId == 1)
                {
                    internalMembers.Add(meetingMember);
                }
                if (meetingMember.Employee.EmployeeTypeId == 2)
                {
                    externalMembers.Add(meetingMember);
                }
            }


            ShowMeetingDetailsVM sm = new ShowMeetingDetailsVM();
            sm.Meeting = meetingRepository.Get(meetingId);
            sm.Employees = employeeRepository.GetAll();
            //sm.meetingMembers = meetingMembers;
            sm.meetingMembers = new AddMemberVM()
            {
                AvailableEmployees = internalMembers,
                AvailableExternalMembers = externalMembers
            };
            sm.AvailableEmployees = AvailableEmployees(meetingId);
            sm.meetingFileUploads = meetingMemberRepository.GetFilesByMeetingId(meetingId).Where(c => c.IsDeleted == false);
            sm.Discussions = discussionRepository.GetAllByMeetingId(meetingId).ToList();
            sm.Implementations = implementationRepository.GetAllByMeetingId(meetingId).ToList();
            //return View(meetingMembers);            
            return View(sm);
        }
        public PartialViewResult MembersPartial(List<SelectedMembersVM> meetingMembers)
        {
            List<MeetingMemberPartialVM> addedMeetingMembers = new List<MeetingMemberPartialVM>();
            //List<MeetingMemberVM> addedMeetingMembers = new List<MeetingMemberVM>();

            if (meetingMembers != null)
            {
                foreach (var item in meetingMembers)
                {
                    //if (item.IsSelected)
                    //{
                    MeetingMember member = new MeetingMember();
                    member.CreatedById = GetEmployee().EmployeeId;
                    member.CreatedDate = DateTime.Now;
                    member.MeetingId = item.MeetingId;
                    member.EmployeeId = item.EmployeeId;
                    var meeting = meetingRepository.Get(item.MeetingId);
                    member.BeginningTime = meeting.BeginningTime;
                    member.EndTime = meeting.EndTime;

                    meetingMemberRepository.Insert(member);

                    //}
                }

            }

            var _meetingMembers =
                meetingMemberRepository.GetMembersByMeetingId(meetingMembers.Select(c => c.MeetingId).FirstOrDefault()).OrderBy(c => c.MeetingMemberId);


            List<MeetingMemberVM> internalMembers = new List<MeetingMemberVM>();
            List<MeetingMemberVM> externalMembers = new List<MeetingMemberVM>();

            foreach (var item in _meetingMembers)
            {
                MeetingMemberPartialVM partialMember = new MeetingMemberPartialVM();
                var employee = employeeRepository.Get(item.EmployeeId);
                partialMember.MeetingId = item.MeetingId;
                partialMember.EmployeeId = item.EmployeeId;
                partialMember.EmployeeName = employee.EmployeeName;
                partialMember.EmployeeType = employee.EmployeeType.EmployeeTypeName;
                partialMember.Department = employee.Department.DepartmentName;
                partialMember.Designation = employee.Designation.DesignationName;
                addedMeetingMembers.Add(partialMember);


                MeetingMemberVM mm = new MeetingMemberVM();
                mm.MeetingId = item.MeetingId;
                mm.EmployeeId = item.EmployeeId;
                mm.Employee = employeeRepository.Get(item.EmployeeId);
                mm.Department = mm.Employee.Department;
                mm.Designation = mm.Employee.Designation;
                if (mm.Employee.EmployeeTypeId == 1)
                {
                    internalMembers.Add(mm);
                }
                if (mm.Employee.EmployeeTypeId == 2)
                {
                    externalMembers.Add(mm);
                }



            }

            AddMemberVM vm = new AddMemberVM()
            {
                MeetingId = meetingMembers.Select(c => c.MeetingId).FirstOrDefault(),
                AvailableEmployees = internalMembers,
                AvailableExternalMembers = externalMembers
            };
            return PartialView(vm);
            //return PartialView(addedMeetingMembers);
        }
        public ActionResult GetVenueName(int venueId)
        {

            var venue = venueRepository.Get(venueId);
            //var list = new List<Venue> {venue};
            //return Json(list, JsonRequestBehavior.AllowGet);

            var result = new { venue.VenueName, venue.VenueId };
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult TodayMeetings()
        {

            var today = DateTime.Today;
            var allMeetings = AllMeetings().Where(c => c.BeginningTime.Date.DayOfYear == today.DayOfYear);
            var dataList = (from meeting in allMeetings

                            select new
                            {
                                meeting.MeetingId,
                                meeting.MeetingName,
                                meeting.BeginningTime,
                                meeting.EndTime,
                                meeting.EmployeeId,
                                meeting.Employee.EmployeeName,
                                meeting.VenueId,
                                VenueName = meeting.VenueId == 0 ? "No Venue Added" : meeting.Venue.VenueName,
                                meeting.MeetingDescription,
                                meeting.Status

                            }).ToList();

            return Json(dataList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AvailableMembers(int meetingId)
        {
            List<MeetingMemberVM> meetingMembers = new List<MeetingMemberVM>();
            List<MeetingMemberVM> externalMembers = new List<MeetingMemberVM>();

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
                if (meetingMember.Employee.EmployeeTypeId == 1)
                {
                    meetingMembers.Add(meetingMember);
                }
                if (meetingMember.Employee.EmployeeTypeId == 2)
                {
                    externalMembers.Add(meetingMember);
                }

            }



            var list = new
            {
                Internals = (from item in meetingMembers
                             select new
                             {
                                 item.EmployeeId,
                                 //item.IsSelected,
                                 item.MeetingId,
                                 EmployeeName = item.Employee.EmployeeName,
                                 Department = item.Employee.Department.DepartmentName,
                                 Designation = item.Designation.DesignationName

                             }).ToList(),
                Externals = (from item in externalMembers
                             select new
                             {
                                 item.EmployeeId,
                                 //item.IsSelected,
                                 item.MeetingId,
                                 EmployeeName = item.Employee.EmployeeName,
                                 Department = item.Employee.Department.DepartmentName,
                                 Designation = item.Designation.DesignationName

                             }).ToList()
            };
            //return meetingMembers;
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult AddMemberPartial(int meetingId)
        {
            List<MeetingMemberVM> meetingMembers = new List<MeetingMemberVM>();
            List<MeetingMemberVM> externalMembers = new List<MeetingMemberVM>();

            var meetings = meetingRepository.GetAll();
            var meeting = meetings.FirstOrDefault(c => c.MeetingId == meetingId);
            var meetingStart = meeting.BeginningTime;
            var meetingEnd = meeting.EndTime;

            var meetingMemberLog = meetingMemberRepository.GetAll();

            var busyEmployeeList = new HashSet<int>(meetingMemberLog.Where(c => c.BeginningTime < meetingEnd && meetingStart < c.EndTime.AddMinutes(30) && c.IsDeleted != true)
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
                if (meetingMember.Employee.EmployeeTypeId == 1)
                {
                    meetingMembers.Add(meetingMember);
                }
                if (meetingMember.Employee.EmployeeTypeId == 2)
                {
                    externalMembers.Add(meetingMember);
                }

            }

            AddMemberVM vm = new AddMemberVM()
            {
                MeetingId = meetingId,
                AvailableEmployees = meetingMembers,
                AvailableExternalMembers = externalMembers
            };

            return PartialView(vm);
        }

        [HttpGet]
        public ActionResult MeetingFileArchive(int id)
        {
            List<MeetingFileUpload> ObjFiles = meetingMemberRepository.GetAllFileByMeetingId(id);

            ViewBag.MeetingId = id;
            return View(ObjFiles);
        }

        [HttpGet]
        public ActionResult DeleteFile(int id, int meetingId)
        {
            int row = 0;
            string filePath = meetingMemberRepository.GetFilePath(id);

            if (filePath != null || filePath != string.Empty)
            {
                if (System.IO.File.Exists(filePath))
                {
                    //System.IO.File.Delete(filePath);
                    row = meetingMemberRepository.DeleteFile(id);
                }

            }
            return RedirectToAction("MeetingFileArchive", new { id = meetingId });

        }
        [HttpGet]
        public ActionResult RestoreFile(int id, int meetingId)
        {
            int row = 0;
            string filePath = meetingMemberRepository.GetFilePath(id);

            if (filePath != null || filePath != string.Empty)
            {
                if (System.IO.File.Exists(filePath))
                {
                    //System.IO.File.Delete(filePath);
                    row = meetingMemberRepository.RestoreFile(id);
                }

            }
            return RedirectToAction("MeetingFileArchive", new { id = meetingId });

        }
        public ActionResult UpdateSchedule(int _meetingId, int employeeId)
        {
            var userEmployeeId = GetEmployee().EmployeeId;
            var meeting = meetingRepository.Get(_meetingId);
            var member = meetingMemberRepository.Get(_meetingId,employeeId);
            member.BeginningTime = meeting.BeginningTime;
            member.EndTime = meeting.EndTime;
            member.ModifiedById = userEmployeeId;
            member.ModifiedDate=DateTime.Now;
            int row = meetingMemberRepository.Edit(member);
            return RedirectToAction("MeetingDetails", "Meeting", new { meetingId = _meetingId });
        }
        public ActionResult CheckVenueAvailable(int meetingId, int presentVenueId, DateTime? beginningTime, DateTime? endTime)
        {
            var meetingStart = beginningTime;
            var meetingEnd = endTime;
            var allMeetings = meetingRepository.GetAll();

            var busyVenueList = new HashSet<int>(allMeetings.Where(c => c.MeetingId != meetingId && c.BeginningTime < meetingEnd && meetingStart < c.EndTime.AddMinutes(30))
                .GroupBy(c => c.VenueId).Select(c => c.FirstOrDefault()).Select(c => c.VenueId)).ToList();
            bool isAvailable = true;
            if (busyVenueList.Count > 0)
            {
                isAvailable = !busyVenueList.Contains(presentVenueId);
            }

            return Json(isAvailable, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckMeetingCallerAvailablity(int meetingId, int presentMeetingCallerId, DateTime? beginningTime, DateTime? endTime)
        {
            var meetingStart = beginningTime;
            var meetingEnd = endTime;
            var meetingMemberLog = meetingMemberRepository.GetAll();

            var busyEmployeeList = new HashSet<int>(meetingMemberLog.Where(c => c.MeetingId != meetingId && c.BeginningTime < meetingEnd && meetingStart < c.EndTime.AddMinutes(30) && c.IsDeleted != true)
                .GroupBy(c => c.EmployeeId).Select(c => c.FirstOrDefault()).Select(c => c.EmployeeId)).ToList();

            bool isAvailable = true;
            if (busyEmployeeList.Count > 0)
            {
                isAvailable = !busyEmployeeList.Contains(presentMeetingCallerId);
            }
            return Json(isAvailable, JsonRequestBehavior.AllowGet);
        }
    }
}

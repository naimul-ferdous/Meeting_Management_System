using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MeetingManagementSystem.Models;
using MeetingManagementSystem.Repositories;
using MeetingManagementSystem.ViewModels;

namespace MeetingManagementSystem.Controllers
{
    public class DiscussionController : Controller
    {
        DiscussionRepository discussionRepository = new DiscussionRepository();
        EmployeeRepository employeeRepository = new EmployeeRepository();
        // GET: Discussion
        public ActionResult Index()
        {
            return View();
        }

        // GET: Discussion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult GetDiscussions(int discussionId)
        {
            var discussion = discussionRepository.Get(discussionId);
            var ds = new List<Discussion>();
            ds.Add(discussion);
            var list = ds.Select(c => new
            {
                DiscussionText = c.DiscussionText,
                DiscussionId = c.DiscussionId
            }).FirstOrDefault();

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        private Employee GetEmployee()
        {
            UserSessionVM userSession = (UserSessionVM)Session["UserSession"];
            var employee = employeeRepository.GetAll().FirstOrDefault(c => c.Email == userSession.User.EmailId);
            return employee;
        }
        // POST: Discussion/Create
        //[HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(DiscussionVM discussion)
        {

            try
            {
                Discussion ds = new Discussion();
                ds = Mapper.Map<Discussion>(discussion);
                ds.CreatedById = GetEmployee().EmployeeId;
                //ds.CreatedBy = GetEmployee();
                ds.CreatedDate = DateTime.Now;
                var isInserted = discussionRepository.Insert(ds);

                if (isInserted <= 0) return Json(isInserted, JsonRequestBehavior.AllowGet);
                var list = discussionRepository.GetAllByMeetingId(ds.MeetingId)
                    .Select(c => new
                    {
                        DiscussionText = c.DiscussionText,
                        EmployeeName = c.ModifiedById == null ? employeeRepository.Get(c.CreatedById).EmployeeName : employeeRepository.Get((int)c.ModifiedById).EmployeeName,
                        DiscussionId = c.DiscussionId
                    }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                Exception ex = new Exception();
                var message = ex.Message;
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            
        }

        // POST: Discussion/Edit/5

        [ValidateInput(false)]
        public ActionResult Edit(DiscussionVM discussion)
        {
            try
            {
                var ds = Mapper.Map<Discussion>(discussion);
                ds.ModifiedById = GetEmployee().EmployeeId;
                ds.ModifiedDate = DateTime.Now;
                var isUpdated = discussionRepository.Edit(ds);

                if (isUpdated <= 0) return Json(isUpdated, JsonRequestBehavior.AllowGet);
                var list = discussionRepository.GetAllByMeetingId(ds.MeetingId)
                    .Select(c => new
                    {
                        DiscussionText = c.DiscussionText,
                        EmployeeName = c.ModifiedById == null ? employeeRepository.Get(c.CreatedById).EmployeeName : employeeRepository.Get((int)c.ModifiedById).EmployeeName,
                        DiscussionId = c.DiscussionId
                    }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);

            }
            catch
            {
                Exception ex = new Exception();
                var message = ex.Message;
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }


        //[HttpPost]
        public ActionResult Delete(int discussionId)
        {
            var ds = discussionRepository.Get(discussionId);
            try
            {
                var isRemoved = discussionRepository.Delete(discussionId);

                if (isRemoved <= 0) return Json(isRemoved, JsonRequestBehavior.AllowGet);

                var list = discussionRepository.GetAllByMeetingId(ds.MeetingId)
                    .Select(c => new
                    {
                        DiscussionText = c.DiscussionText,
                        EmployeeName = c.ModifiedById== null ? employeeRepository.Get(c.CreatedById).EmployeeName : employeeRepository.Get((int)c.ModifiedById).EmployeeName,
                        DiscussionId = c.DiscussionId
                    }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                Exception ex = new Exception();
                var message = ex.Message;
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}

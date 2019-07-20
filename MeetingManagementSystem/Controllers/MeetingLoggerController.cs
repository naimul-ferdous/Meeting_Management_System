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
     public class MeetingLoggerController : Controller
    {
        MeetingManagementDbContext context = new MeetingManagementDbContext();
        MeetingLoggerRepository meetingLoggerRepository = new MeetingLoggerRepository();
        // GET: Meeting
        public ActionResult Index()
        {
            var dataList = (from meeting in context.Meetings
                            join emp in context.Employees on meeting.EmployeeId equals emp.EmployeeId
                            join venue in context.Venues on meeting.VenueId equals venue.VenueId
                            
                            select new
                            {
                                meeting.MeetingId,
                                meeting.MeetingName,
                                meeting.BeginningTime,
                                meeting.EndTime,
                                //meeting.Date,
                                meeting.EmployeeId,
                                emp.EmployeeName,
                                meeting.VenueId,
                                venue.VenueName,
                                meeting.MeetingDescription,
                                meeting.Status

                            }).ToList();

            return Json(dataList, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult AddLog(MeetingLoggerVM meetingLogger)
        public ActionResult AddLog(MeetingLoggerVM meetingLogger)
        {
            try
            {

                int row = meetingLoggerRepository.Insert(meetingLogger);
                return Json(row, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return View();
            }
        }
        // GET: Meeting/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Meeting/Create
        //public ActionResult Create()
        //{

        //    return View();
        //}

        // POST: Meeting/Create
       // [HttpPost]
        //public ActionResult Create(MeetingLoggerVM meetingLogger)
        public ActionResult Create(MeetingLoggerVM meetingLogger)
        {
            try
            {

                int row = meetingLoggerRepository.Insert(meetingLogger);
                return Json(row, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return View();
            }
        }

        // GET: Meeting/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Meeting/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Meeting/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Meeting/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

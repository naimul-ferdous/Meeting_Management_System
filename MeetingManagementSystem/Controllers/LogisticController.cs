using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetingManagementSystem.Repositories;
using MeetingManagementSystem.Models;

namespace MeetingManagementSystem.Controllers
{
    public class LogisticController : Controller
    {
        LogisticRepository logisticRepository = new LogisticRepository();

        VenueRepository venueRepository = new VenueRepository();
        // GET: Logistics
        public ActionResult GetVenue()
        {
          
            return Json(venueRepository.GetAll(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Logistic()
        {
            return View();
        }
        public ActionResult Index()
        {
            return Json(logisticRepository.GetAll(), JsonRequestBehavior.AllowGet);
        }

        // GET: Logistics/Details/5
        public ActionResult Details(int id)
        {
            return Json(logisticRepository.Get(id), JsonRequestBehavior.AllowGet); ;
        }

        // GET: Logistics/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: Logistics/Create
        [HttpPost]
        public ActionResult Create(Logistic logistic)
        {
            return Json(logisticRepository.Insert(logistic), JsonRequestBehavior.AllowGet);
        }

        // GET: Logistics/Edit/5
        public ActionResult Edit(int id)
        {
            return Json(logisticRepository.Get(id), JsonRequestBehavior.AllowGet);
        }

        // POST: Logistics/Edit/5
        [HttpPost]
        public ActionResult Edit(Logistic logistic)
        {
            return Json(logisticRepository.Edit(logistic), JsonRequestBehavior.AllowGet);
        }

        // GET: Logistics/Delete/5
        public ActionResult Delete(int id)
        {
            return Json(logisticRepository.Delete(id), JsonRequestBehavior.AllowGet);
        }

        // POST: Logistics/Delete/5
        
    }
}

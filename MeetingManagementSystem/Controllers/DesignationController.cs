using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetingManagementSystem.Repositories;
using MeetingManagementSystem.Models;

namespace MeetingManagementSystem.Controllers
{
    public class DesignationController : Controller
    {
        DesignationRepository repository = new DesignationRepository();
        public ActionResult Designation()
        {
            return View();
        }

        public ActionResult Create(Designation designation)
        {
            int rowAff = repository.Insert(designation);
            return Json(rowAff, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            var list = repository.GetAll();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(Designation designation)
        {
            int rowAff = repository.Edit(designation);
            return Json(rowAff, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int designationId)
        {
            Designation designation = new Designation() {DesignationId = designationId };
            int row = repository.Delete(designationId);
            return Json(row, JsonRequestBehavior.AllowGet);
        }

    }
}
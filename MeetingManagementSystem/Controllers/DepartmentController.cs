using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using MeetingManagementSystem.Repositories;
using MeetingManagementSystem.Models;

namespace MeetingManagementSystem.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentRepository repository = new DepartmentRepository();

        public ActionResult Department()
        {
            return View();
        }
        public ActionResult Create(Department department)
        {
            int rowAff = repository.Insert(department);
            return Json(rowAff, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            var list = repository.GetAll();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(Department department)
        {
            int rowAff = repository.Edit(department);
            return Json(rowAff, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int departmentId)
        {
            Department department = new Department { DepartmentId = departmentId };
            int row = repository.Delete(departmentId);
            return Json(row, JsonRequestBehavior.AllowGet);
        }

    }
}
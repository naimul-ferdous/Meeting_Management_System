using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using MeetingManagementSystem.Models;
using MeetingManagementSystem.Repositories;

namespace MeetingManagementSystem.Controllers
{
    public class EmployeeTypeController : Controller
    {

        EmployeeTypeRepository employeeTypeRepository = new EmployeeTypeRepository();

        public ActionResult EmployeeType()
        {
            return View();
        }
        public ActionResult Create(EmployeeType employeeType)
        {
            int row = employeeTypeRepository.Insert(employeeType);
            return Json(row, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(EmployeeType employeeType)
        {
            int row = employeeTypeRepository.Edit(employeeType);
            return Json(row, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int employeeTypeId)
        {
           
            int row = employeeTypeRepository.Delete(employeeTypeId);
            return Json(row, JsonRequestBehavior.AllowGet);
        }
      
        public ActionResult GetEmployeeTypeInfo()
        {
            var allEmployeeTypes = employeeTypeRepository.GetAll();
            var list = (from empType in allEmployeeTypes
                        select new
                        {
                            empType.EmployeeTypeId,
                            empType.EmployeeTypeName
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            var dataList = employeeTypeRepository.GetAll();

            return Json(dataList, JsonRequestBehavior.AllowGet);
        }
    }
}
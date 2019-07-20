using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using MeetingManagementSystem.Models;
using MeetingManagementSystem.Repositories;
using MeetingManagementSystem.ViewModels;

namespace MeetingManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {

        EmployeeRepository employeeRepository = new EmployeeRepository();
        DepartmentRepository departmentRepository = new DepartmentRepository();
        EmployeeTypeRepository employeeTypeRepository = new EmployeeTypeRepository();
        OfficeRepository officeRepository = new OfficeRepository();
        DesignationRepository designationRepository = new DesignationRepository();


        public ActionResult Employee()
        {
            return View();
        }

        [ValidateAjax]
        public ActionResult Create(EmployeeVM employee)
        {
            int row = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    Employee emp = new Employee()
                    {
                        Address = employee.Address,
                        OfficeId = employee.OfficeId,
                        DepartmentId = employee.DepartmentId,
                        DesignationId = employee.DesignationId,
                        Email = employee.Email,
                        EmployeeName = employee.EmployeeName,
                        EmployeeOfficialId = employee.EmployeeOfficialId,
                        EmployeeTypeId = employee.EmployeeTypeId,
                        PhoneNumber = employee.PhoneNumber,
                        Password = employee.Password,
                        IsActive = employee.IsActive

                    };
                    row = employeeRepository.Insert(emp);
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return Json(row, JsonRequestBehavior.AllowGet);
        }
        [ValidateAjax]
        public ActionResult Edit(EmployeeVM employee)
        {
            int row = 0;
            if (ModelState.IsValid)
            {
                Employee emp = new Employee()
                {
                    EmployeeId =(int)employee.EmployeeId,
                    Address = employee.Address,
                    OfficeId = employee.OfficeId,
                    DepartmentId = employee.DepartmentId,
                    DesignationId = employee.DesignationId,
                    Email = employee.Email,
                    EmployeeName = employee.EmployeeName,
                    EmployeeOfficialId = employee.EmployeeOfficialId,
                    EmployeeTypeId = employee.EmployeeTypeId,
                    PhoneNumber = employee.PhoneNumber,
                    Password = employee.Password,
                    IsActive = employee.IsActive

                };
                row = employeeRepository.Edit(emp);
            }
            //int row = employeeRepository.Edit(employee);
            return Json(row, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int employeeId)
        {
            int row = employeeRepository.Delete(employeeId);
            return Json(row, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDesignationInfo()
        {
            var allDesignations = designationRepository.GetAll();
            var list = (from dsg in allDesignations
                        select new
                        {
                            dsg.DesignationId,
                            dsg.DesignationName
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetOfficeInfo()
        {
            var allOffices = officeRepository.GetAll();
            var list = (from office in allOffices
                        select new
                        {
                            office.OfficeId,
                            office.OfficeName
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
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
            var allEmployees = employeeRepository.GetAll();
            var allDepartments = departmentRepository.GetAll();
            var allEmployeeTypes = employeeTypeRepository.GetAll();
            var allOffices = officeRepository.GetAll();
            var allDesignations = designationRepository.GetAll();

            var dataList = (from emp in allEmployees
                            join dpt in allDepartments on emp.DepartmentId equals dpt.DepartmentId
                            join empType in allEmployeeTypes on emp.EmployeeTypeId equals empType.EmployeeTypeId
                            join office in allOffices on emp.OfficeId equals office.OfficeId
                            join desig in allDesignations on emp.DesignationId equals desig.DesignationId

                            select new
                            {
                                emp.EmployeeOfficialId,
                                emp.EmployeeId,
                                emp.EmployeeName,
                                emp.Email,
                                emp.PhoneNumber,
                                emp.Password,
                                emp.Address,
                                emp.DepartmentId,
                                dpt.DepartmentName,
                                emp.DesignationId,
                                desig.DesignationName,
                                emp.OfficeId,
                                office.OfficeName,
                                emp.EmployeeTypeId,
                                empType.EmployeeTypeName,
                                emp.IsActive
                            }).ToList();

            return Json(dataList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllEmployees()
        {
            var employees = employeeRepository.GetAll();

            var list = (from emp in employees
                        select new
                        {
                            emp.EmployeeId,
                            emp.EmployeeName
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

       public JsonResult IsEmailAlreadyExist(string Email, int? EmployeeId)
       {
            using (MeetingManagementDbContext db = new MeetingManagementDbContext())
            {
                try
                {
                    if (EmployeeId != null)
                    {
                        var tag = db.Employees.FirstOrDefault(m => m.Email == Email && m.EmployeeId == (int)EmployeeId);
                        if (tag != null)
                        {
                            return Json(true, JsonRequestBehavior.AllowGet);
                        }
                        tag = db.Employees.FirstOrDefault(m => m.Email == Email);
                        if (tag != null)
                        {
                            return Json(false, JsonRequestBehavior.AllowGet);
                        }
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var tag = db.Employees.Single(m => m.Email == Email);
                        if (tag != null)
                        {
                            return Json(false, JsonRequestBehavior.AllowGet);
                        }
                    }


                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult IsOfficialIdAvailable(string EmployeeOfficialId,int? EmployeeId)
        {
            using (MeetingManagementDbContext db = new MeetingManagementDbContext())
            {
                try
                {
                    if (EmployeeId != null)
                    {
                        var tag = db.Employees.FirstOrDefault(m => m.EmployeeOfficialId == EmployeeOfficialId && m.EmployeeId==(int)EmployeeId);
                        if (tag != null)
                        {
                            return Json(true, JsonRequestBehavior.AllowGet);
                        }
                        tag = db.Employees.FirstOrDefault(m => m.EmployeeOfficialId == EmployeeOfficialId);
                        if (tag != null)
                        {
                            return Json(false, JsonRequestBehavior.AllowGet);
                        }
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var tag = db.Employees.Single(m => m.EmployeeOfficialId == EmployeeOfficialId);
                        if (tag != null)
                        {
                            return Json(false, JsonRequestBehavior.AllowGet);
                        }
                    }
                    

                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
        }

    }
}
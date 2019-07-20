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
    public class SecRoleController : Controller
    {

        SecRoleRepository secRoleRepository = new SecRoleRepository();
        // GET: SecRole
        public ActionResult SecRole()
        {
            return View();
        }

        public ActionResult Index()
        {
            var list = secRoleRepository.GetAll();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create([Bind(Include = "RoleName,Status")]SecRole secRole)
        {
            ActionResult rtn = Json(0, JsonRequestBehavior.DenyGet);
            try
            {
                secRole.CreatedBy = 1;
                secRole.CreatedDate = DateTime.Now;
                int row = secRoleRepository.Insert(secRole);
                return Json(row, JsonRequestBehavior.AllowGet);

            }
            catch (Exception exception)
            {
                rtn = Json(-100, JsonRequestBehavior.DenyGet);
            }
            return rtn;
           
        }

        public ActionResult Edit([Bind(Include = "SecRoleId,RoleName,Status")]SecRole secRole)
        {
            secRole.SecRoleId = secRole.SecRoleId;
            secRole.ModifiedBy = 2;
            secRole.ModifiedDate = DateTime.Now;
            int row = secRoleRepository.Edit(secRole);
            return Json(row, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int secRoleId)
        {
            int row = secRoleRepository.Delete(secRoleId);
            return Json(row, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllSecRole()
        {
            var secRoles = secRoleRepository.GetAll();

            var list = (secRoles.Select(secRole => new
            {
                secRole.SecRoleId,
                secRole.RoleName,
                secRole.Status,
                secRole.CreatedBy,
                CreatedDate = secRole.CreatedDate.ToString("f"),
                secRole.ModifiedBy,
                //ModifiedDate = secRole.ModifiedDate.ToString("f")
            })).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRoleInfo()
        {
            var allRole = secRoleRepository.GetAll();
            var list = (from role in allRole
                select new
                {
                    role.SecRoleId,
                     role.RoleName
                }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
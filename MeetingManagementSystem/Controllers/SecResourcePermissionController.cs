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
    public class SecResourcePermissionController : Controller
    {

        MeetingManagementDbContext dbContext = new MeetingManagementDbContext();
        
        SecRoleRepository secRoleRepository = new SecRoleRepository();
        SecResourcePermissionRepository secResourcePermissionRepository = new SecResourcePermissionRepository();

        // GET: SecResourcePermission
        public ActionResult SecResourcePermission()
        {
            return View();
        }


        public ActionResult Create(SecResourcePermission secResourcePermission)
        {
            int row = secResourcePermissionRepository.Insert(secResourcePermission);
            return Json(row, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(SecResourcePermission secResourcePermission)
        {
            int row = secResourcePermissionRepository.Edit(secResourcePermission);
            return Json(row, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int secResourcePermissionId)
        {
            int row = secResourcePermissionRepository.Delete(secResourcePermissionId);
            return Json(row, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllSecResourcePermission()
        {
            var resourcePermission = dbContext.SecResourcePermissions.ToList();
            var roleList = dbContext.SecRoles.ToList();
            var secResource = dbContext.SecResources.ToList();

            var dataList = (from srp in resourcePermission
                join role in roleList on srp.SecRoleId equals role.SecRoleId
                join resource in secResource on srp.SecResourceId equals resource.SecResourceId

                select new
                {
                    srp.SecResourcePermissionId,
                    srp.SecRoleId,
                    role.RoleName,
                    srp.SecResourceId,
                    Name=resource.FileName,
                    srp.FileName,
                    srp.MenuName,
                    srp.DisplayName,
                    srp.ModuleId,
                    srp.Order,
                    srp.Level,
                    srp.ActionUrl,
                    srp.Status,
                   
                }).ToList();

            return Json(dataList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRoleInfo()
        {
            var allRole = secRoleRepository.GetAll();
            var list = (from role in allRole
                select new
                {
                    role.SecRoleId,
                    RoleName = role.RoleName
                }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetResourceInfo()
        {
            var allResource = secResourcePermissionRepository.GetAll();
            var list = (from resource in allResource
                select new
                {
                    resource.SecResourceId,
                    FileNames = resource.FileName
                }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ResourceWithRole(int id)
        {
            MeetingManagementDbContext context = new MeetingManagementDbContext();
            List<SecResourcePermission> resourcePermissions = context.SecResourcePermissions.Where(r => r.SecRoleId == id).ToList();
            List<SecRolePermission> rolePermissions = context.SecRolePermissions.Where(r => r.SecRoleId == id).ToList();
            var list = (from resource in resourcePermissions
                        join role in rolePermissions
                        on resource.SecResourceId equals role.SecResourceId
                        select new
                        {
                            SecResourcePermissionId = resource.SecResourcePermissionId,

                            SecRoleId = id,

                            SecResourceId = resource.SecResourceId,

                            FileName = resource.FileName,
                            MenuName = resource.MenuName,
                            DisplayName = resource.DisplayName,
                            ModuleId = resource.ModuleId,
                            Order = resource.Order,
                            Level = resource.Level,
                            ActionUrl = resource.ActionUrl,
                            Status = resource.Status,
                            SecRolePermissionId = role.SecRolePermissionId,
                            Add = role.Add,
                            Edit = role.Edit,
                            Delete = role.Delete
                        }).ToList();
            List<ResourceRolePermissionVm> resourceRolePermissionVms = new List<ResourceRolePermissionVm>();
            foreach (var resourcePermission in list)
            {
                ResourceRolePermissionVm resourceRolePermissionVm = new ResourceRolePermissionVm
                {
                    SecResourcePermissionId = resourcePermission.SecResourcePermissionId,
                    SecRoleId = resourcePermission.SecRoleId,
                    SecResourceId = resourcePermission.SecResourceId,
                    FileName = resourcePermission.FileName,
                    MenuName = resourcePermission.MenuName,
                    DisplayName = resourcePermission.DisplayName,
                    ModuleId = resourcePermission.ModuleId,
                    Order = resourcePermission.Order,
                    Level = resourcePermission.Level,
                    ActionUrl = resourcePermission.ActionUrl,
                    Status = resourcePermission.Status,
                    SecRolePermissionId = resourcePermission.SecRolePermissionId,
                    Add = resourcePermission.Add,
                    Edit = resourcePermission.Edit,
                    Delete = resourcePermission.Delete

                };
                resourceRolePermissionVms.Add(resourceRolePermissionVm);
            }
            return PartialView(resourceRolePermissionVms);
            // return Json(resourceRolePermissionVms.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetData(int id)
        {
            //int id = 7;
            MeetingManagementDbContext context = new MeetingManagementDbContext();
            List<SecResourcePermission> resourcePermissions = context.SecResourcePermissions.Where(r => r.SecRoleId == id).ToList();
            List<SecRolePermission> rolePermissions = context.SecRolePermissions.Where(r => r.SecRoleId == id).ToList();
            var list = (from resource in resourcePermissions
                        join role in rolePermissions
                            on resource.SecResourceId equals role.SecResourceId
                        select new
                        {
                            SecResourcePermissionId = resource.SecResourcePermissionId,

                            SecRoleId = id,

                            SecResourceId = resource.SecResourceId,

                            FileName = resource.FileName,
                            text = "<input type='checkbox'>" + resource.MenuName,
                            //text = resource.MenuName,
                            DisplayName = resource.DisplayName,
                            ModuleId = resource.ModuleId,
                            Order = resource.Order,
                            Level = resource.Level,
                            ActionUrl = resource.ActionUrl,
                            Status = resource.Status,
                            SecRolePermissionId = role.SecRolePermissionId,
                            Add = role.Add,
                            Edit = role.Edit,
                            Delete = role.Delete
                        }).ToList();
            //var list = (from resource in resourcePermissions
            //            join role in rolePermissions
            //                on resource.SecResourceId equals role.SecResourceId select new
            //    {
            //        SecResourcePermissionId = resource.SecResourcePermissionId,

            //                    text = resource.MenuName,

            //                }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ResourceWithRole_V2()
        {
            return View();


          
        }

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetingManagementSystem.Models;
using MeetingManagementSystem.Repositories;

namespace MeetingManagementSystem.Controllers
{
    public class SecUserRoleController : Controller
    {
        MeetingManagementDbContext dbContext = new MeetingManagementDbContext();
        SecUserRoleRepository secUserRoleRepository = new SecUserRoleRepository();
        SecUserRepository secUserRepository = new SecUserRepository();
        SecRoleRepository secRoleRepository = new SecRoleRepository();
        // GET: SecUserRole
        public ActionResult SecUserRole()
        {
            return View();
        }

        //public ActionResult Index()
        //{
        //    var list = secUserRoleRepository.GetAll();
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult Create([Bind(Include = "SecUserId,SecRoleId")]SecUserRole secUserRole)
        {
            secUserRole.CreatedBy = 1;
            secUserRole.CreatedDate = DateTime.Now;
            int row = secUserRoleRepository.Insert(secUserRole);
            return Json(row, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit([Bind(Include = "SecUserRoleId,SecRoleId")]SecUserRole secUserRole)
        {
            //secUserRole.SecUserRoleId = secUserRole.SecUserRoleId;
            secUserRole.ModifiedBy = 2;
            secUserRole.ModifiedDate = DateTime.Now;
            int row = secUserRoleRepository.Edit(secUserRole);
            return Json(row, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int secUserRoleId)
        {
            int row = secUserRoleRepository.Delete(secUserRoleId);
            return Json(row, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            var userList = dbContext.SecUsers.ToList();
            var roleList = dbContext.SecRoles.ToList();
            var secUserRole = dbContext.SecUserRoles.ToList();
            var dataList = (from ur in secUserRole
                            join secUser in userList on ur.SecUserId equals secUser.SecUserId
                            join secRole in roleList on ur.SecRoleId equals secRole.SecRoleId
                            select new
                            {
                                ur.SecUserRoleId,
                                secUser.SecUserId,
                                UserName =secUser.LoginName,
                                ur.SecRoleId,
                                secRole.RoleName,
                                ur.CreatedBy,
                                CreatedDate = ur.CreatedDate.ToString("f"),
                                ur.ModifiedBy,
                                //ModifiedDate = ur.ModifiedDate.ToString("f")

                            }).ToList();

            return Json(dataList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUserInfo()
        {
            var allUser = UsersWithoutRole();

            var list = (from user in allUser
                        select new
                        {
                            user.SecUserId,
                            UserName = user.LoginName
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
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


        private IEnumerable<SecUser> UsersWithoutRole()
        {
            var userWithRoleList = new HashSet<int>(secUserRoleRepository.GetAll().Select(c=>c.SecUserId)).ToList();
            
            var allUser = secUserRepository.GetAll().ToList();
            IEnumerable<SecUser> usersWithoutRole;
            if (userWithRoleList.Count > 0)
            {
                usersWithoutRole = allUser.Where(x => !userWithRoleList.Contains(x.SecUserId)).ToList();
            }
            else
            {
                usersWithoutRole = allUser;
            }

            return usersWithoutRole;
        }

    }
}
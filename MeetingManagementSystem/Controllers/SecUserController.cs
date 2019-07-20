using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetingManagementSystem.Models;
using MeetingManagementSystem.Repositories;

namespace MeetingManagementSystem.Controllers
{
    public class SecUserController : Controller
    {
        SecUserRepository secUserRepository = new SecUserRepository();
        // GET: SecUser
        public ActionResult SecUser()
        {
            return View();
        }

        public ActionResult Index()
        {
            var list = secUserRepository.GetAll();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Create([Bind(Include = "LoginName,Password,EmailId,Status")]SecUser secUser)
        {
            ActionResult rtn = Json(0, JsonRequestBehavior.DenyGet);

            try
            {
                    secUser.CreatedBy = 1;
                    secUser.CreatedDate = DateTime.Now;
                    int row = secUserRepository.Insert(secUser);
                    return Json(row, JsonRequestBehavior.AllowGet);
                
            }
            catch (Exception exception)
            {
                rtn = Json(-100, JsonRequestBehavior.DenyGet);
            }
            return rtn;
        }


        //public ActionResult Create([Bind(Include = "LoginName,Password,EmailId,Status")]SecUser secUser)

        //{
        //    secUser.CreatedBy = 1;
        //    secUser.CreatedDate = DateTime.Now;
        //    int row = secUserRepository.Insert(secUser);
        //    return Json(row, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult Edit([Bind(Include = "SecUserId,LoginName,Password,EmailId,Status")]SecUser secUser)
        {
            secUser.SecUserId = secUser.SecUserId;
            secUser.ModifiedBy = 2;
            secUser.ModifiedDate = DateTime.Now;
            int row = secUserRepository.Edit(secUser);
            return Json(row, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int secUserId)
        {
            int row = secUserRepository.Delete(secUserId);
            return Json(row, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllSecUser()
        {
            var secUsers = secUserRepository.GetAll();

            var list = (secUsers.Select(secUser => new
            {
                secUser.SecUserId,
                secUser.LoginName,
                secUser.Password,
                secUser.EmailId,
                secUser.Status,
                secUser.CreatedBy,
                CreatedDate = secUser.CreatedDate.ToString("f"),
                secUser.ModifiedBy,
                //ModifiedDate = secUser.ModifiedDate.ToString("f")
            })).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}
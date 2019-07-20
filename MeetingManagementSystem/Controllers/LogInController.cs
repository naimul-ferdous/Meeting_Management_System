using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetingManagementSystem.Models;
using MeetingManagementSystem.ViewModels;

namespace MeetingManagementSystem.Controllers
{
    public class LogInController : Controller
    {
        MeetingManagementDbContext context=new MeetingManagementDbContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(SecUser secUser)
        {
            var user = context.SecUsers.FirstOrDefault(c => c.EmailId == secUser.EmailId && c.Password == secUser.Password);
            
            
            if (user != null)
            {
                var role = context.SecUserRoles.FirstOrDefault(c => c.SecUserId == user.SecUserId);
                UserSessionVM session=new UserSessionVM();
                session.User = user;
                session.Role = role;
                session.RolePermissions = context.SecRolePermissions.Where(c=>c.SecRoleId==role.SecRoleId).ToList();
                session.LogInTime=DateTime.Now;
                Session["UserSession"] = session;

                return RedirectToAction("Dashboard", "Home");
            }

            TempData["Message"] = "Sorry! You are not authorized.";
            return View();
        }
        public ActionResult LogOut()
        {
            //UserSessionVM session = new UserSessionVM();
            //session =(UserSessionVM) Session["UserSession"];

            Session["UserSession"] = null;
            return RedirectToAction("Index","Home");
        }
    }
}
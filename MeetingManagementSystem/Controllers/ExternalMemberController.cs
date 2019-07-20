using MeetingManagementSystem.Models;
using MeetingManagementSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeetingManagementSystem.Controllers
{
    public class ExternalMemberController : Controller
    {
        ExternalMemberRepository externalMemberRepository = new ExternalMemberRepository();

        // GET: ExternalMember
        public ActionResult ExternalMember()
        {
            return View();
        }

        public ActionResult Index()
        {
            var list = externalMemberRepository.GetAll();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(ExternalMember externalMember)
        {
            int row = externalMemberRepository.Insert(externalMember);
            return Json(row, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(ExternalMember externalMember)
        {
            int row = externalMemberRepository.Edit(externalMember);
            return Json(row, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int externalMemberId)
        {
            ExternalMember externalMember = new ExternalMember { ExternalMemberId = externalMemberId };
            int row = externalMemberRepository.Delete(externalMemberId);
            return Json(row, JsonRequestBehavior.AllowGet);
        }
    }
}
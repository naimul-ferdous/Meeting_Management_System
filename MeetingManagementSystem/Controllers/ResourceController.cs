using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetingManagementSystem.Models;

namespace MeetingManagementSystem.Controllers
{
    public class ResourceController : Controller
    {
        // GET: Resource
        public ActionResult Index()
        {
            return View(new MeetingManagementDbContext().SecResources.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(SecResource resource)
        {
            MeetingManagementDbContext context = new MeetingManagementDbContext();
            context.SecResources.Add(resource);
            context.SaveChanges();
            return RedirectToAction("Index", "Resource");
        }
    }
}
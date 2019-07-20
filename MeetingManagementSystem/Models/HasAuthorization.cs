using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MeetingManagementSystem.ViewModels;


namespace MeetingManagementSystem.Models
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class HasAuthorizationAttribute : AuthorizeAttribute
    {
        public int AccessLevel { get; set; }

        private int ReturnCode { set; get; }
        private bool ReturnStatus { set; get; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            MeetingManagementDbContext db = new MeetingManagementDbContext();

            var session = (UserSessionVM)HttpContext.Current.Session["UserSession"];
            if (session == null)
            {
                //session timeout
                ReturnCode = -1000;
                ReturnStatus = false;
                return false;

            }



            //string privilegeLevels = string.Join("", GetUserRights(httpContext.User.Identity.Name.ToString())); // Call another method to get rights of the user from DB

            var routeData = System.Web.Routing.RouteTable.Routes.GetRouteData(HttpContext.Current.Request.RequestContext.HttpContext);
            var values = routeData.Values;
            var controller = values["controller"];
            var action = values["action"];
            var secResourcePermission = db.SecResourcePermissions.FirstOrDefault(r => r.SecRoleId == session.Role.SecRoleId && r.MenuName == controller && r.Status == true);



            if (AccessLevel == 1)
            {//add
                //if (action == "Create")
                //{
                if (secResourcePermission != null)
                {
                    var rolePermission = db.SecRolePermissions.FirstOrDefault(r =>
                        r.SecRoleId == secResourcePermission.SecRoleId &&
                        r.SecResourceId == secResourcePermission.SecResourceId);
                    if (rolePermission.Add == true)
                    {
                        return true;
                    }
                    else
                    {
                        ReturnCode = 1001;
                        ReturnStatus = false;
                        return false;
                    }

                }
                ReturnCode = 1001;
                ReturnStatus = false;
                return false;

                //}
            }

            if (AccessLevel == 2)
            {//Edit
             //if (action == "Edit")
             //{
                if (secResourcePermission != null)
                {
                    var rolePermission = db.SecRolePermissions.FirstOrDefault(r =>
                        r.SecRoleId == secResourcePermission.SecRoleId &&
                        r.SecResourceId == secResourcePermission.SecResourceId);
                    if (rolePermission.Edit == true)
                    {
                        return true;
                    }
                    else
                    {
                        ReturnCode = 1001;
                        ReturnStatus = false;
                        return false;
                    }

                }
                ReturnCode = 1001;
                ReturnStatus = false;
                return false;
                //}
            }
            if (AccessLevel == 3)
            {//Delete
             //if (action == "Delete")
             //{
                if (secResourcePermission != null)
                {
                    var rolePermission = db.SecRolePermissions.FirstOrDefault(r =>
                        r.SecRoleId == secResourcePermission.SecRoleId &&
                        r.SecResourceId == secResourcePermission.SecResourceId);
                    if (rolePermission.Delete == true)
                    {
                        return true;
                    }
                    else
                    {
                        ReturnCode = 1001;
                        ReturnStatus = false;
                        return false;
                    }

                }
                ReturnCode = 1001;
                ReturnStatus = false;
                return false;
                //}
            }
            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            if (!ReturnStatus)
            {
                if (context.HttpContext.Request.IsAjaxRequest())
                {
                    if (ReturnCode == 1001)
                    {
                        context.HttpContext.Response.StatusCode = 403;
                        context.Result = new JsonResult { Data = ReturnCode, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                    }
                    //you have no resource permission
                    context.HttpContext.Response.StatusCode = 403;
                    context.Result = new JsonResult { Data = ReturnCode, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                else
                {
                    switch (ReturnCode)
                    {
                        case -1000:
                            //session timeout
                            context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index" }));
                            break;
                        case 1001:

                            //you have no resource permission on page redirect/load
                            context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "ErrorResourcePermission" }));
                            break;


                    }

                }
            }
        }
    }

}
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeetingManagementSystem.Models;
using MeetingManagementSystem.ViewModels;
using Microsoft.Ajax.Utilities;  


namespace MeetingManagementSystem.Controllers
{
    public class ResourceTreeController : Controller
    {
        public JsonResult Get(int roleId)
        {
            List<SecResourcePermission> resources;
            List<SecRolePermission> rolePermissions;
            List<ResourceTreeVm> records = new List<ResourceTreeVm>();
            using (MeetingManagementDbContext context = new MeetingManagementDbContext())
            {
               
                resources = context.SecResourcePermissions.Where(q => q.SecRoleId == roleId).ToList();
                rolePermissions = context.SecRolePermissions.Where(q => q.SecRoleId == roleId).ToList();

                var roleResource = (from resource in resources
                                    join role in rolePermissions
                        on resource.SecResourceId equals role.SecResourceId
                                    select new
                                    {
                                        resource.SecResourceId,
                                     
                                        resource.MenuName,
                                        resource.Order,
                                        resource.Level,
                                        resource.SecResourcePermissionId,
                                        resource.Status,
                                        role.SecRolePermissionId,
                                        role.Add,
                                        role.Edit,
                                        role.Delete

                                    }).ToList();

                foreach (var l in roleResource.Where(l => l.Level == 0).OrderBy(l => l.Order))
                {
                    ResourceTreeVm resource = new ResourceTreeVm();
                    //resource.id = l.SecResourceId;
                    resource.id = l.SecResourcePermissionId;
                    resource.text = l.MenuName;
                    //resource.@checked = l.Status;
                    //if (l.Status == false)
                    //{
                       // resource.@checked = l.Status;
                    //}
                    if (GetChildren_V2(resources, l.SecResourceId).Count == 0)
                    {

                        resource.children = new List<ResourceTreeVm>();
                        var childAdd = new ResourceTreeVm();
                        childAdd.id = l.SecRolePermissionId;
                        childAdd.text = "Add";
                        childAdd.@checked = l.Add;
                        resource.children.Add(childAdd);
                        var childEdit = new ResourceTreeVm();
                        childEdit.id = l.SecRolePermissionId;
                        childEdit.text = "Edit";
                        childEdit.@checked = l.Edit;
                        resource.children.Add(childEdit);
                        var childDelete = new ResourceTreeVm();
                        childDelete.id = l.SecRolePermissionId;
                        childDelete.text = "Delete";
                        childDelete.@checked = l.Delete;
                        resource.children.Add(childDelete);
                    }
                    else
                    {
                        resource.children = GetChildren_V2(resources, l.SecResourceId);
                    }
                    records.Add(resource);

                }

            }

            //foreach (var rr in records)
            //{
            //    rr.@checked = resources.First(f => f.SecResourcePermissionId == rr.id).Status;
            //}

            return this.Json(records.ToList(), JsonRequestBehavior.AllowGet);
        }

        private List<ResourceTreeVm> GetChildren_V2(List<SecResourcePermission> roleResource, int parentId)
        {
            List<ResourceTreeVm> records = new List<ResourceTreeVm>();

            foreach (var l in roleResource.Where(l => l.Level == parentId).OrderBy(l => l.Order))
            {

                ResourceTreeVm resource = new ResourceTreeVm();
                //resource.id = l.SecResourceId;
                resource.id = l.SecResourcePermissionId;
                resource.text = l.MenuName;
                //resource.@checked = l.Status;
                //if (l.Status==false)
                //{
                //    resource.@checked = false;
                //}
                if (GetChildren_V2(roleResource, l.SecResourceId).Count == 0)
                {

                    using (MeetingManagementDbContext context = new MeetingManagementDbContext())
                    {
                        var childRolePermission = context.SecRolePermissions.FirstOrDefault(c => c.SecRoleId == l.SecRoleId && c.SecResourceId == l.SecResourceId);
                        if (childRolePermission != null)
                        {

                            resource.children = new List<ResourceTreeVm>();
                            var childAdd = new ResourceTreeVm();
                            childAdd.id = childRolePermission.SecRolePermissionId;
                            childAdd.text = "Add";
                            childAdd.@checked = childRolePermission.Add;
                            resource.children.Add(childAdd);
                            var childEdit = new ResourceTreeVm();
                            childEdit.id = childRolePermission.SecRolePermissionId;
                            childEdit.text = "Edit";
                            childEdit.@checked = childRolePermission.Edit;
                            resource.children.Add(childEdit);
                            var childDelete = new ResourceTreeVm();
                            childDelete.id = childRolePermission.SecRolePermissionId;
                            childDelete.text = "Delete";
                            childDelete.@checked = childRolePermission.Delete;
                            resource.children.Add(childDelete);

                        }

                    }
                }
                else
                {
                    resource.children = GetChildren_V2(roleResource, l.SecResourceId);
                }
                records.Add(resource);

            }

            return records.ToList();
        }
        public JsonResult GetMenus()
        {
            UserSessionVM userSession = (UserSessionVM)Session["UserSession"];
            var roleId = userSession.Role.SecRoleId;

            List<SecResourcePermission> resources;
            List<SecRolePermission> rolePermissions;
            List<MenuTree> records = new List<MenuTree>();
            using (MeetingManagementDbContext context = new MeetingManagementDbContext())
            {
                resources = context.SecResourcePermissions.Where(q => q.SecRoleId == roleId && q.Status==true).ToList();
                rolePermissions = context.SecRolePermissions.Where(q => q.SecRoleId == roleId).ToList();

                var roleResource = (from resource in resources
                                    join role in rolePermissions
                        on resource.SecResourceId equals role.SecResourceId
                                    select new
                                    {
                                        resource.SecResourceId,
                                        resource.MenuName,
                                        resource.Order,
                                        resource.Level,
                                        resource.ActionUrl,
                                        resource.DisplayName,
                                        resource.SecResourcePermissionId,
                                        resource.Status,
                                        role.SecRolePermissionId,


                                    }).ToList();

                foreach (var l in roleResource.Where(l => l.Level == 0).OrderBy(l => l.Order))
                {
                    MenuTree Tree = new MenuTree()
                    {
                        text = l.DisplayName,
                        nodes = GetChildren(resources, l.SecResourceId)
                    };
                    if (l.Level == 0 && GetChildren(resources, l.SecResourceId).Count==0)
                    {
                        Tree.href = l.ActionUrl;
                    }
                    else
                    {
                        Tree.href = "#";

                    }
                    records.Add(Tree);
                }

            }

            return this.Json(records.ToList(), JsonRequestBehavior.AllowGet);
        }
        private List<MenuTree> GetChildren(List<SecResourcePermission> resourcesList, int parentId)
        {
            return resourcesList.Where(l => l.Level == parentId).OrderBy(l => l.Order)
                .Select(l => new MenuTree()
                {
                    text = l.DisplayName,
                    href = l.ActionUrl,
                    nodes = GetChildren(resourcesList, l.SecResourceId)
                }).ToList();
        }

        [HttpPost]
        public JsonResult SaveCheckedNodes(List<int> checkedIds)
        {
            if (checkedIds == null)
            {
                checkedIds = new List<int>();
            }
            using (MeetingManagementDbContext context = new MeetingManagementDbContext())
            {
                var resourcesList = context.SecResourcePermissions.ToList();
                foreach (var resource in resourcesList)
                {
                    resource.Status = checkedIds.Contains(resource.SecResourcePermissionId);
                }
                context.SaveChanges();
            }

            return this.Json(true);
        }

       


        [HttpPost]
        public JsonResult SaveCheckedNodes_V2(List<ResourceTreeVm> resourceRoleList)
        {
            if (resourceRoleList == null)
            {
                resourceRoleList = new List<ResourceTreeVm>();
            }
            using (MeetingManagementDbContext context = new MeetingManagementDbContext())
            {
                foreach (var item in resourceRoleList)
                {
                    //resource permission update
                    string mystring = item.text;
                    string res = mystring.GetLast(13);
                    if (res == "AddEditDelete")
                    {
                        var resource =
                            context.SecResourcePermissions.FirstOrDefault(c => c.SecResourcePermissionId == item.id);
                        resource.Status = item.@checked;

                    }
                    //role permission update
                    var rolePermission = context.SecRolePermissions.FirstOrDefault(c => c.SecRolePermissionId == item.id);
                    if (rolePermission != null)
                    {
                        if (item.text == "Add")
                        {
                            if (item.@checked == true)
                            {
                                rolePermission.Add = true;

                            }
                            else
                            {
                                rolePermission.Add = false;
                            }
                        }
                        if (item.text == "Edit")
                        {
                            if (item.@checked == true)
                            {
                                rolePermission.Edit = true;
                            }
                            else
                            {
                                rolePermission.Edit = false;
                            }
                        }
                        if (item.text == "Delete")
                        {
                            if (item.@checked == true)
                            {
                                rolePermission.Delete = true;
                            }
                            else
                            {
                                rolePermission.Delete = false;
                            }
                        }
                    }



                }
                context.SaveChanges();
            }

            return this.Json(true);
        }


    }
    public static class StringExtension
    {
        public static string GetLast(this string source, int tail_length)
        {
            if (tail_length >= source.Length)
                return source;
            return source.Substring(source.Length - tail_length);
        }
    }
}
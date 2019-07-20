using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingManagementSystem.ViewModels
{
    public class ResourceTreeVm
    {
        public int id { get; set; }

        public string text { get; set; }

        public bool @checked { get; set; }

        public virtual List<ResourceTreeVm> children { get; set; }

        public RolePermissionVm RolePermission { get; set; }
    }
}
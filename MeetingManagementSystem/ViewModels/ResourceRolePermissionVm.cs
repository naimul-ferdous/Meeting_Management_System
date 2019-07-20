using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingManagementSystem.ViewModels
{
    public class ResourceRolePermissionVm
    {
        public int SecResourcePermissionId { get; set; }
        
        public int SecRoleId { get; set; }
        
        public int SecResourceId { get; set; }

        public string FileName { get; set; }
        public string MenuName { get; set; }
        public string DisplayName { get; set; }
        public int ModuleId { get; set; }
        public int Order { get; set; }
        public int Level { get; set; }
        public string ActionUrl { get; set; }
        public bool Status { get; set; }
        public int SecRolePermissionId { get; set; }

        
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }

    }
}
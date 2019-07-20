using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingManagementSystem.ViewModels
{
    public class RolePermissionVm
    {
        public int SecRolePermissionId { get; set; }
        

        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
    }
}
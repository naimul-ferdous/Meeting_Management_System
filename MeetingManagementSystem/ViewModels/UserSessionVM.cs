using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MeetingManagementSystem.Models;

namespace MeetingManagementSystem.ViewModels
{
    public class UserSessionVM
    {
        public SecUser User { get; set; }
        public SecUserRole Role { get; set; }
        public DateTime LogInTime { get; set; }
        public List<SecRolePermission> RolePermissions { get; set; }
    }
}
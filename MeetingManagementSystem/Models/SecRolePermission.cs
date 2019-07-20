using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingManagementSystem.Models
{
    public class SecRolePermission
    {
        public int  SecRolePermissionId { get; set; }
        
        public virtual SecResource SecResource { get; set; }
        public int SecResourceId { get; set; }
        public virtual SecRole SecRole { get; set; }
        public int SecRoleId { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
       
    }
}
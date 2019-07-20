using System;
using System.Collections.Generic;

namespace MeetingManagementSystem.Models
{
    public class SecResourcePermission
    {
        public int SecResourcePermissionId { get; set; }
        public virtual SecRole SecRole { get; set; }
        public int SecRoleId { get; set; }
        public virtual SecResource SecResource { get; set; }
        public int SecResourceId { get; set; }
        
        public string FileName { get; set; }
        public string MenuName { get; set; }
        public string DisplayName { get; set; }
        public int ModuleId { get; set; }
        public int Order { get; set; }
        //Parent - Child Determiner
        public int Level { get; set; }
        public string ActionUrl { get; set; }
        public bool Status { get; set; }


        public virtual SecResourcePermission Parent { get; set; }

        public virtual List<SecResourcePermission> Children { get; set; }

    }
}

using System;

namespace MeetingManagementSystem.Models
{
    public class SecResource
    {
        public int SecResourceId { get; set; }
        public string FileName { get; set; }
        public string MenuName { get; set; }
        public string DisplayName { get; set; }
        public int ModuleId { get; set; }
        public int Order { get; set; }
        public int Level { get; set; }
        public string ActionUrl { get; set; }
        public bool Status { get; set; }


       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingManagementSystem.ViewModels
{

    public class MenuTree
    {
        public string text { get; set; }
        public string href { get; set; }
        public virtual List<MenuTree> nodes { get; set; }
    }
}
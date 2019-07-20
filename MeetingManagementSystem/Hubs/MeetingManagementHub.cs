using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace MeetingManagementSystem.Hubs
{
    public class MeetingManagementHub : Hub
    {
        public static void Show()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MeetingManagementHub>();
            context.Clients.All.displayMyMeetings();
        }
    }
}
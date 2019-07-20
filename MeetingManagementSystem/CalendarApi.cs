using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MeetingManagementSystem.Models;
using System.IO;

using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MeetingManagementSystem
{
    public class CalendarApi
    {
        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        //static string ApplicationName = "MeetingManagementSystem";
        public void AddEvent(Meeting meeting,List<MeetingMember> meetingMember)
        {

            List<EventAttendee> ev = new List<EventAttendee>();
            foreach(var item in meetingMember)
            {
                EventAttendee eve = new EventAttendee();
                eve.Email = item.Employee.Email;
                ev.Add(eve);
            }
            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                            new ClientSecrets
                            {
                                ClientId = "835753453200-elh85iedu1m4b83g8j2etca94qqm37q5.apps.googleusercontent.com",
                                ClientSecret = "AEIVa59jGlVvTfxb8o92g-8I",
                            },
                            new[] { CalendarService.Scope.Calendar },
                            "user",
                            CancellationToken.None).Result;

            // Create the service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Calendar API Sample",
            });


            Event myEvent = new Event
            {
                Summary = meeting.MeetingName,
                Location = meeting.Venue.VenueName,
                Description = meeting.MeetingDescription,
                Start = new EventDateTime()
                {
                    DateTime =meeting.BeginningTime,
                    TimeZone = "Asia/Dhaka"
                },
                End = new EventDateTime()
                {
                    DateTime = meeting.EndTime,
                    TimeZone = "Asia/Dhaka"
                },
                Recurrence = new String[] {
      "RRULE:FREQ=WEEKLY;COUNT=1"
  },
                Attendees = ev
            };

            Event recurringEvent = service.Events.Insert(myEvent, "primary").Execute();
        }
    }
}
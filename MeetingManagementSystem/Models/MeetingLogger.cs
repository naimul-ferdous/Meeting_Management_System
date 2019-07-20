using System;
using System.Collections.Generic;
namespace MeetingManagementSystem.Models
{
    public class MeetingLogger
    {
        
        public int MeetingLoggerId { get; set; }
        public int MeetingId { get; set; }
        public Meeting Meeting { get; set; }
        public int VenueId { get; set; }
        public Venue Venue { get; set; }
        public DateTime BeginningTime { set; get; }
        public DateTime EndTime { set; get; }
        public string Status { get; set; }
        
    }
}

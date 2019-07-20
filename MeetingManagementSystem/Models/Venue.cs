using System.Collections.Generic;

namespace MeetingManagementSystem.Models
{
    public class Venue
    {
        public int VenueId { set; get; }
        public string VenueName { set; get; }
        public string Capacity { get; set; }
        public int VenueType { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public ICollection<Logistic>Logistics { get; set; }
        public ICollection<Meeting>Meetings { get; set; }
    }
}

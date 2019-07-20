namespace MeetingManagementSystem.Models
{
    public class Logistic
    {
        public int LogisticId { get; set; }
        public string LogisticName { get; set; }
        public string Availbale { get; set; }
        public int VenueId { get; set; }
        public Venue Venue { get; set; }
    }
}

namespace MeetingManagementSystem.Models
{
    public class Entertainment
    {
        public int EntertainmentId { get; set; }
        public string EntertainmentItem { get; set; }
        public int Quantity { get; set; }
        public int MeetingId { get; set; }
        public Meeting Meeting { get; set; }
    }
}

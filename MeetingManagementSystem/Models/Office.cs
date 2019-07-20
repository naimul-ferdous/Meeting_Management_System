namespace MeetingManagementSystem.Models
{
    public class Office
    {
        public int OfficeId { set; get; }
        public string OfficeName { get; set; }
        public int AddressId { get; set; }
       
        //public ICollection<Venue> Venues { get; set; }

    }
}

using System.Collections.Generic;

namespace MeetingManagementSystem.Models
{
    public class Address
    {
        
        public int AddressId { get; set; }
        public string HouseNo { get; set; }
        public string RoadNo { get; set; }
        public string Block { get; set; }
        public string Area { get; set; }
        public string PostCode { get; set; }
        public int CountryId { get; set; }
        //public int Country { get; set; }
        public int DistrictId { set; get; }
        //public int District { get; set; }
        public virtual ICollection<Venue> Venues { get; set; }
    }
}

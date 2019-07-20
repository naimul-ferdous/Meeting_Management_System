using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingManagementSystem.ViewModels
{
    public class VenueVM
    {
        public int VenueId { set; get; }
        public string VenueName { set; get; }
        public string Capacity { get; set; }
        public int VenueType { get; set; }
        public string HouseNo { get; set; }
        public string RoadNo { get; set; }
        public string Block { get; set; }
        public string Area { get; set; }
        public string PostCode { get; set; }
        public int DistrictId { set; get; }
        public int CountryId { set; get; }
    }
}
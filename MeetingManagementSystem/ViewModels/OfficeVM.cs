using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingManagementSystem.ViewModels
{
    public class OfficeVM
    {
        public int OfficeId { set; get; }
        public string OfficeName { get; set; }
        public string HouseNo { get; set; }
        public string RoadNo { get; set; }
        public string Block { get; set; }
        public string Area { get; set; }
        public string PostCode { get; set; }
        public int DistrictId { set; get; }
        public int CountryId { set; get; }

    }
}
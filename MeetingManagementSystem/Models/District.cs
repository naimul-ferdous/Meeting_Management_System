using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MeetingManagementSystem.Models
{
    [Table("Districts")]
    public class District
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}
using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Web;

namespace MeetingManagementSystem.Models
{
    //public class Country
    //{
    //    public int CountryId { get; set; }
    //    public string CountryName { get; set; }
    //    public ICollection<City> Citys { get; set; }
[Table("Countries")]
    public class Country
    {
        public int CountryId { set; get; }
        public string CountryName { set; get; }

        public virtual ICollection<District> Districts { get; set; }
    }
}
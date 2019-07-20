using System;
using System.Collections.Generic;
namespace MeetingManagementSystem.Models
{
    public class Meeting
    {
        
        public int MeetingId { get; set; }
        public string MeetingName { get; set; }
        public DateTime BeginningTime { set; get; }
        public DateTime EndTime { set; get; }
        public string MeetingDescription { get; set; }
        public int Status { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public int VenueId { get; set; }
        public virtual Venue Venue { get; set; }
        public bool Approval { get; set; }
        public ICollection<Entertainment> Entertainments { get; set; }
        public ICollection<MeetingAgenda> MeetingAgendas { get; set; }
        public ICollection<MeetingFileUpload> MeetingFileUploads { get; set; } 
        public ICollection<MeetingResult> MeetingResults { get; set; }


        public int CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}

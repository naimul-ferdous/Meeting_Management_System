using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MeetingManagementSystem.Models
{
    public class MeetingFileUpload
    {
        public int MeetingFileUploadId { get; set; }
        public string FileName { get; set; }
        //public byte[] FileContent { get; set; }
        public string FilePath { get; set; }
        //[NotMapped]
        //public HttpPostedFileBase Files { get; set; }
        public int MeetingId { get; set; }
        public virtual Meeting Meeting { get; set; }
        public bool IsDeleted { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace JobApplicationSystem.DAL.Models
{
    public partial class Interview
    {
        public int InterviewId { get; set; }
        public DateTime? Time { get; set; }
        public string? Location { get; set; }
        public string? Feedback { get; set; }
        public int? Application { get; set; }

        public virtual Application? ApplicationNavigation { get; set; }
    }
}

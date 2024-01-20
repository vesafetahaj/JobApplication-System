using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobApplicationSystem.DAL.Models
{
    public partial class Interview
    {
        public int InterviewId { get; set; }

        [Required(ErrorMessage = "Please enter the interview time.")]
        public DateTime? Time { get; set; }

        [Required(ErrorMessage = "Please enter the location.")]
        public string? Location { get; set; }
        public string? Feedback { get; set; }
        public int? Application { get; set; }

        public virtual Application? ApplicationNavigation { get; set; }
    }
}

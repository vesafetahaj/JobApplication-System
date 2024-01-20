using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobApplicationSystem.DAL.Models
{
    public partial class Feedback
    {
        public int FeedbackId { get; set; }

        [Required(ErrorMessage = "You have to rate.")]
        public int? Rating { get; set; }

        [Required(ErrorMessage = "Please add a comment.")]
        public string? Comments { get; set; }
        public string? AspNetUser { get; set; }

        public virtual AspNetUser? AspNetUserNavigation { get; set; }
    }
}

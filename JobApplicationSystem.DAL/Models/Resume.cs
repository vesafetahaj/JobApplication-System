using System;
using System.Collections.Generic;

namespace JobApplicationSystem.DAL.Models
{
    public partial class Resume
    {
        public int ResumeId { get; set; }
        public string? ResumeName { get; set; }
        public byte[]? ResumeContent { get; set; }
    }
}

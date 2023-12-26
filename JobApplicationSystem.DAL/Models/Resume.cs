using System;
using System.Collections.Generic;

namespace JobApplicationSystem.DAL.Models
{
    public partial class Resume
    {
        public Resume()
        {
            Applicants = new HashSet<Applicant>();
        }

        public int ResumeId { get; set; }
        public string? ResumeName { get; set; }
        public byte[]? ResumeContent { get; set; }

        public virtual ICollection<Applicant> Applicants { get; set; }
    }
}

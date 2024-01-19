using System;
using System.Collections.Generic;

namespace JobApplicationSystem.DAL.Models
{
    public partial class Application
    {
        public Application()
        {
            Interviews = new HashSet<Interview>();
        }

        public int ApplicationId { get; set; }
        public string? Education { get; set; }
        public string? Experience { get; set; }
        public int? Applicant { get; set; }
        public int? Job { get; set; }
        public string? Resume { get; set; }
        public string? Status { get; set; }

        public string? Date { get; set; }

        public virtual Applicant? ApplicantNavigation { get; set; }
        public virtual Job? JobNavigation { get; set; }
        public virtual ICollection<Interview> Interviews { get; set; }
    }
}

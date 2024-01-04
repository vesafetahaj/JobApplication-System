using System;
using System.Collections.Generic;

namespace JobApplicationSystem.DAL.Models
{
    public partial class Applicant
    {
        public Applicant()
        {
            Applications = new HashSet<Application>();
        }

        public int ApplicantId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? Address { get; set; }
        public string? Image { get; set; }
        public string? User { get; set; }
        public string? Education { get; set; }

        public virtual AspNetUser? UserNavigation { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
    }
}

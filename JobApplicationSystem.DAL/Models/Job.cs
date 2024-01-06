using System;
using System.Collections.Generic;

namespace JobApplicationSystem.DAL.Models
{
    public partial class Job
    {
        public Job()
        {
            Applications = new HashSet<Application>();
        }

        public int JobId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Education { get; set; }
        public string? Experience { get; set; }
        public int? NumberPosition { get; set; }
        public DateTime? LastDateToApply { get; set; }
        public string? Company { get; set; }
        public string? CompanyLogo { get; set; }
        public string? Address { get; set; }
        public int? Employer { get; set; }
        public string? Salary { get; set; }

        public virtual Employer? EmployerNavigation { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
    }
}

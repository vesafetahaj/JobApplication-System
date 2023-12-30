using System;
using System.Collections.Generic;

namespace JobApplicationSystem.DAL.Models
{
    public partial class Employer
    {
        public Employer()
        {
            Jobs = new HashSet<Job>();
        }

        public int EmployerId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? Address { get; set; }
        public string? Image { get; set; }
        public string? User { get; set; }
        public string? Company { get; set; }

        public virtual AspNetUser? UserNavigation { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}

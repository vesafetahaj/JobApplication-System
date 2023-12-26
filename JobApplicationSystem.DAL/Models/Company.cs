using System;
using System.Collections.Generic;

namespace JobApplicationSystem.DAL.Models
{
    public partial class Company
    {
        public Company()
        {
            Employers = new HashSet<Employer>();
        }

        public int CompanyId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }

        public virtual ICollection<Employer> Employers { get; set; }
    }
}

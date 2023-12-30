using System;
using System.Collections.Generic;

namespace JobApplicationSystem.DAL.Models
{
    public partial class Administrator
    {
        public int AdminId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? User { get; set; }
        public string? Image { get; set; }
        public string? Address { get; set; }

        public virtual AspNetUser? UserNavigation { get; set; }
    }
}

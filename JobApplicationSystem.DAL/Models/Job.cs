using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobApplicationSystem.DAL.Models
{
    public partial class Job
    {
        public Job()
        {
            Applications = new HashSet<Application>();
        }

        public int JobId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Education is required.")]
        public string? Education { get; set; }

        [Required(ErrorMessage = "Experience is required.")]
        public string? Experience { get; set; }

        [Required(ErrorMessage = "NumberPosition is required.")]
        public int? NumberPosition { get; set; }

        [Required(ErrorMessage = "LastDateToApply is required.")]
        public DateTime? LastDateToApply { get; set; }

        public string? Company { get; set; }

        [Required(ErrorMessage = "CompanyLogo is required.")]
        public string? CompanyLogo { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string? Address { get; set; }

        public int? Employer { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        public string? Salary { get; set; }

        public virtual Employer? EmployerNavigation { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
    }
}

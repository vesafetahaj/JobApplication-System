using JobApplicationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationSystem.BAL.Services
{
    public interface IApplicantService
    {
        Task<Applicant> GetApplicantDetailsAsync(string userId);
        Task<bool> SavePersonalInfoAsync(Applicant applicant);
        bool HasProvidedPersonalInfo(string userId);
        Task<Applicant> GetApplicantByIdAsync(int applicantId);
        Task<bool> EditPersonalInfoAsync(Applicant applicant);
        Task<IEnumerable<Application>> GetApplicationsByUserIdAsync(string userId);
    }
}


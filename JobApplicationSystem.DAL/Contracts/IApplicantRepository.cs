using JobApplicationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationSystem.DAL.Contracts
{
    public interface IApplicantRepository<Applicant>
    {
        Task<Applicant> GetApplicantAsync(string userId);
        Task<bool> SaveApplicantAsync(Applicant applicant);
        bool HasProvidedPersonalInfo(string userId);
        Task<Applicant> GetApplicantByIdAsync(int applicantId);
        Task<bool> EditPersonalInfoAsync(Applicant applicant);
    }
}

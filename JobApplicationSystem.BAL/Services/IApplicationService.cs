using JobApplicationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationSystem.BAL.Services
{
    public interface IApplicationService
    {
        Task<bool> AddApplicationAsync(Application application);
        Task<IEnumerable<Application>> GetAllApplicationsAsync();
        Task<Application> GetApplicationByIdAsync(int applicationId);
        Task<bool> UpdateApplicationAsync(int applicationId, Application updatedApplication);
        Task<bool> DeleteApplicationAsync(int applicationId);
        bool CheckIfApplicantApplied(int? applicantId, int? jobId);
        Task<IEnumerable<Applicant>> GetApplicantsForJobAsync(int jobId);

    }
}

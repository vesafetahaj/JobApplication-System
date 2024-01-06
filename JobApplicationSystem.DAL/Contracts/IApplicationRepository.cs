using JobApplicationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationSystem.DAL.Contracts
{
    public interface IApplicationRepository<Application>
    {
        // Create
        Task<bool> AddApplicationAsync(Application application);

        // Read
        Task<IEnumerable<Application>> GetAllApplicationsAsync();
        Task<Application> GetApplicationByIdAsync(int applicationId);

        // Update
        Task<bool> UpdateApplicationAsync(int applicationId, Application updatedApplication);

        // Delete
        Task<bool> DeleteApplicationAsync(int applicationId);
        bool CheckIfApplicantApplied(int? applicantId, int? jobId);
        Task<IEnumerable<Application>> GetApplicationsByUserIdAsync(string userId);
        Task<IEnumerable<Application>> GetApplicationsByJobIdAsync(int jobId);
    }
}

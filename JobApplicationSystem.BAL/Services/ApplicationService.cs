using JobApplicationSystem.DAL.Contracts;
using JobApplicationSystem.DAL.Data;
using JobApplicationSystem.DAL.Models;
using JobApplicationSystem.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationSystem.BAL.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository<Application> _applicationRepository;
      

        public ApplicationService(IApplicationRepository<Application> applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<bool> AddApplicationAsync(Application application)
        {
            
            return await _applicationRepository.AddApplicationAsync(application);
        }

        public async Task<IEnumerable<Application>> GetAllApplicationsAsync()
        {
            return await _applicationRepository.GetAllApplicationsAsync();
        }

        public async Task<Application> GetApplicationByIdAsync(int applicationId)
        {
            return await _applicationRepository.GetApplicationByIdAsync(applicationId);
        }

        public async Task<bool> UpdateApplicationAsync(int applicationId, Application updatedApplication)
        {
            return await _applicationRepository.UpdateApplicationAsync(applicationId, updatedApplication);
        }

        public async Task<bool> DeleteApplicationAsync(int applicationId)
        {
            return await _applicationRepository.DeleteApplicationAsync(applicationId);
        }
        public bool CheckIfApplicantApplied(int? applicantId, int? jobId)
        {
            return _applicationRepository.CheckIfApplicantApplied(applicantId, jobId);
        }
        public async Task<IEnumerable<Applicant>> GetApplicantsForJobAsync(int jobId)
        {
            var applications = await _applicationRepository.GetApplicationsByJobIdAsync(jobId);

            var applicants = applications.Select(application => application.ApplicantNavigation);

            return applicants;
        }
    }
}

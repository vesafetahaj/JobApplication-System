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
            if (application.Applicant == null || application.Job == null)
            {
                throw new ArgumentException("Application must have a valid applicant and job reference");
            }

            if (CheckIfApplicantApplied(application.Applicant, application.Job))
            {
                throw new InvalidOperationException("Applicant has already applied for this job");
            }
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
            if (updatedApplication.Applicant == null || updatedApplication.Job == null)
            {
                throw new ArgumentException("Updated application must have a valid applicant and job reference");
            }

            if (CheckIfApplicantApplied(updatedApplication.Applicant, updatedApplication.Job))
            {
                throw new InvalidOperationException("Applicant has already applied for the updated job");
            }
            return await _applicationRepository.UpdateApplicationAsync(applicationId, updatedApplication);
        }

        public async Task<bool> DeleteApplicationAsync(int applicationId)
        {
            return await _applicationRepository.DeleteApplicationAsync(applicationId);
        }
        public bool CheckIfApplicantApplied(int? applicantId, int? jobId)
        {
            if (applicantId == null || jobId == null)
            {
                throw new ArgumentException("Both applicantId and jobId must be provided for application check");
            }
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

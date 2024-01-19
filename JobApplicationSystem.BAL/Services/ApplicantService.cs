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
    public class ApplicantService : IApplicantService
    {
        private readonly IApplicantRepository<Applicant> _applicantRepository;
        private readonly IApplicationRepository<Application> _applicationRepository;


        public ApplicantService(IApplicantRepository<Applicant> applicantRepository, IApplicationRepository<Application> applicationRepository)
        {
            _applicantRepository = applicantRepository;
            _applicationRepository = applicationRepository;
        }
        public async Task<Applicant> GetApplicantDetailsAsync(string userId)
        {
            return await _applicantRepository.GetApplicantAsync(userId);
        }

        public bool HasProvidedPersonalInfo(string userId)
        {
            return _applicantRepository.HasProvidedPersonalInfo(userId);
        }
        public async Task<Applicant> GetApplicantByIdAsync(int applicantId)
        {
            return await _applicantRepository.GetApplicantByIdAsync(applicantId);
        }
        public async Task<bool> SavePersonalInfoAsync(Applicant applicant)
        {
            return await _applicantRepository.SaveApplicantAsync(applicant);
        }
        public async Task<bool> EditPersonalInfoAsync(Applicant applicant)
        {
            return await _applicantRepository.EditPersonalInfoAsync(applicant);
        }
        public async Task<IEnumerable<Application>> GetApplicationsByUserIdAsync(string userId)
        {
            return await _applicationRepository.GetApplicationsByUserIdAsync(userId);
        }

    }
}

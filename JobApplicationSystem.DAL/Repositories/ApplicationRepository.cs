using JobApplicationSystem.DAL.Contracts;
using JobApplicationSystem.DAL.Data;
using JobApplicationSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationSystem.DAL.Repositories
{
    public class ApplicationRepository : IApplicationRepository<Application>
    {
        private readonly JobApplicationSystemContext _dbContext;

        public ApplicationRepository(JobApplicationSystemContext dbContext)
        {
            _dbContext = dbContext;
        }
     
        public async Task<bool> AddApplicationAsync(Application application)
        {
            try
            {
                _dbContext.Add(application);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<Application> GetApplicationByIdAsync(int applicationId)
        {
            return await _dbContext.Applications.FindAsync(applicationId);
        }
        public async Task<IEnumerable<Application>> GetAllApplicationsAsync()
        {
            return await _dbContext.Applications.ToListAsync();
        }

        public async Task<bool> UpdateApplicationAsync(int applicationId, Application updatedApplication)
        {
            var existingApplication = await _dbContext.Applications.FindAsync(applicationId);

            if (existingApplication == null)
                return false;

            existingApplication.Education = updatedApplication.Education;
            existingApplication.Experience = updatedApplication.Experience;
            existingApplication.Resume = updatedApplication.Resume;
            existingApplication.Job = updatedApplication.Job;
            existingApplication.Applicant = updatedApplication.Applicant;

            await _dbContext.SaveChangesAsync();
            return true;
        }



        public async Task<bool> DeleteApplicationAsync(int applicationId)
        {
            var applicationToDelete = await _dbContext.Applications.FindAsync(applicationId);

            if (applicationToDelete == null)
                return false;

            _dbContext.Applications.Remove(applicationToDelete);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public bool CheckIfApplicantApplied(int? applicantId, int? jobId)
        {
            return _dbContext.Applications.Any(a => a.Applicant == applicantId && a.Job == jobId);
     
        }
        public async Task<IEnumerable<Application>> GetApplicationsByUserIdAsync(string userId)
        {
            return await _dbContext.Applications.Include(a => a.Interviews)
                .Where(a => a.ApplicantNavigation.User == userId).Include(a => a.JobNavigation)
                .ToListAsync();
        }
        public async Task<IEnumerable<Application>> GetApplicationsByJobIdAsync(int jobId)
        {
            return await _dbContext.Applications
                .Include(a => a.ApplicantNavigation).Include(a => a.Interviews)
                .Where(a => a.Job == jobId)
                .ToListAsync();
        }
    }
}

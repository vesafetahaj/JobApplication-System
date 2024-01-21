using JobApplicationSystem.DAL.Contracts;
using JobApplicationSystem.DAL.Data;
using JobApplicationSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationSystem.DAL.Repositories
{
    public class JobRepository : IJobRepository<Job>
    {
        private readonly JobApplicationSystemContext _dbcontext;

        public JobRepository(JobApplicationSystemContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<int> AddJobAsync(Job job)
        {
            _dbcontext.Jobs.Add(job);
            await _dbcontext.SaveChangesAsync();
            return job.JobId;
        }
        public async Task<Job> GetJobByIdAsync(int jobId)
        {
            return await _dbcontext.Jobs.FindAsync(jobId);
        }
        public async Task<IEnumerable<Job>> GetAllJobsAsync()
        {
            return await _dbcontext.Jobs.Include(a=>a.EmployerNavigation).ToListAsync();
        }

        public async Task<bool> UpdateJobAsync(int jobId, Job updatedJob)
        {
            var existingJob = await _dbcontext.Jobs.FindAsync(jobId);

            if (existingJob == null)
                return false;

            existingJob.Title = updatedJob.Title;
            existingJob.Description = updatedJob.Description;
            existingJob.Education = updatedJob.Education;
            existingJob.Experience = updatedJob.Experience;
            existingJob.NumberPosition = updatedJob.NumberPosition;
            existingJob.LastDateToApply = updatedJob.LastDateToApply;
            existingJob.CompanyLogo = updatedJob.CompanyLogo;
            existingJob.Address = updatedJob.Address;
            existingJob.Employer = updatedJob.Employer;
            existingJob.Salary = updatedJob.Salary;

            await _dbcontext.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteJobAsync(int jobId)
        {
            var jobToDelete = await _dbcontext.Jobs.FindAsync(jobId);

            if (jobToDelete == null)
                return false;

            _dbcontext.Jobs.Remove(jobToDelete);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<Job>> GetJobsByEmployerIdAsync(int employerId)
        {
            return await _dbcontext.Jobs
                .Where(j => j.Employer == employerId)
                .ToListAsync();
        }
        
    }
}

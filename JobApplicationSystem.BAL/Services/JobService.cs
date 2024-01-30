using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobApplicationSystem.DAL.Contracts;
using JobApplicationSystem.DAL.Models;

namespace JobApplicationSystem.BAL.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository<Job> _jobRepository;

        public JobService(IJobRepository<Job> jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<int> AddJobAsync(Job job)
        {
            if (job.Title.Length < 5)
            {
                throw new ArgumentException("Job title must be at least 5 characters long");
            }
            return await _jobRepository.AddJobAsync(job);
        }

        public async Task<IEnumerable<Job>> GetAllJobsAsync()
        {
            return await _jobRepository.GetAllJobsAsync();
        }

        public async Task<Job> GetJobByIdAsync(int jobId)
        {
            return await _jobRepository.GetJobByIdAsync(jobId);
        }

        public async Task<bool> UpdateJobAsync(int jobId, Job updatedJob)
        {
            var existingJob = await _jobRepository.GetJobByIdAsync(jobId);
            if (existingJob == null)
            {
                return false; 
            }
            return await _jobRepository.UpdateJobAsync(jobId, updatedJob);
        }

        public async Task<bool> DeleteJobAsync(int jobId)
        {
            return await _jobRepository.DeleteJobAsync(jobId);
        }

        public async Task<IEnumerable<Job>> GetJobsByEmployerIdAsync(int employerId)
        {
            return await _jobRepository.GetJobsByEmployerIdAsync(employerId);
        }

        public async Task<IEnumerable<Job>> SearchJobsAsync(string searchQuery)
        {
            if (searchQuery == null)
            {

                return Enumerable.Empty<Job>();
            }

            var allJobs = await GetAllJobsAsync();

            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                throw new ArgumentException("Search query cannot be empty or null");
            }

            var searchResults = allJobs.Where(job => job.Title.Contains(searchQuery)
            || job.Company.Contains(searchQuery));
            return searchResults.ToList();
        }


    }
}

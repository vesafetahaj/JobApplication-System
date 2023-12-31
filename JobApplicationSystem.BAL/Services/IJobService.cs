using JobApplicationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationSystem.BAL.Services
{
    public interface IJobService
    {
        Task<int> AddJobAsync(Job job);
        Task<IEnumerable<Job>> GetAllJobsAsync();
        Task<Job> GetJobByIdAsync(int jobId);
        Task<bool> UpdateJobAsync(int jobId, Job updatedJob);
        Task<bool> DeleteJobAsync(int jobId);
        Task<IEnumerable<Job>> GetJobsByEmployerIdAsync(int employerId);
        Task<IEnumerable<Job>> SearchJobsAsync(string searchTerm);
    }
}

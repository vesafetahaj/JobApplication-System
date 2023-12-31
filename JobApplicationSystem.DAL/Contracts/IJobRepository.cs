using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationSystem.DAL.Contracts
{
    public interface IJobRepository<Job>
    {
        // Create
        Task<int> AddJobAsync(Job job);

        // Read
        Task<IEnumerable<Job>> GetAllJobsAsync();
        Task<Job> GetJobByIdAsync(int jobId);

        // Update
        Task<bool> UpdateJobAsync(int jobId, Job updatedJob);

        // Delete
        Task<bool> DeleteJobAsync(int jobId);
        Task<IEnumerable<Job>> GetJobsByEmployerIdAsync(int employerId);
    }
}

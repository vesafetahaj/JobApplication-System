using System.Collections.Generic;
using System.Threading.Tasks;
using JobApplicationSystem.DAL.Models;

namespace JobApplicationSystem.DAL.Repositories
{
    public interface IInterviewRepository<T>
    {
        Task ScheduleInterviewAsync(Interview interview);
        Task<List<Interview>> GetInterviewsByApplicationIdAsync(int applicationId);
        Task<Interview> GetInterviewByIdAsync(int interviewId);
        Task UpdateInterviewAsync(Interview interview);
        Task DeleteInterviewAsync(int interviewId);
        Task<List<Interview>> GetScheduledInterviewsForEmployerAsync(int employerId);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using JobApplicationSystem.DAL.Models;

namespace JobApplicationSystem.BAL.Services
{
    public interface IInterviewService
    {
        Task ScheduleInterviewAsync(Interview interview);
        Task<List<Interview>> GetInterviewsByApplicationIdAsync(int applicationId);
        Task<Interview> GetInterviewByIdAsync(int interviewId);
        Task UpdateInterviewAsync(Interview interview);
        Task DeleteInterviewAsync(int interviewId);
        Task<List<Interview>> GetScheduledInterviewsForEmployerAsync(int employerId);
        Task<bool> HasConflictAsync(Interview newInterview);
    }
}
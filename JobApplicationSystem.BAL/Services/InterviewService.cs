using System.Collections.Generic;
using System.Threading.Tasks;
using JobApplicationSystem.DAL.Contracts;
using JobApplicationSystem.DAL.Data;
using JobApplicationSystem.DAL.Models;
using JobApplicationSystem.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationSystem.BAL.Services
{
    public class InterviewService : IInterviewService
    {
        private readonly IInterviewRepository<Interview> _interviewRepository;

        private readonly JobApplicationSystemContext _dbContext;
        public InterviewService(IInterviewRepository<Interview> interviewRepository, JobApplicationSystemContext dbContext)
        {
            _interviewRepository = interviewRepository;
            _dbContext = dbContext;
        }

        public async Task ScheduleInterviewAsync(Interview interview)
        {
            
            await _interviewRepository.ScheduleInterviewAsync(interview);
        }

        public async Task<List<Interview>> GetInterviewsByApplicationIdAsync(int applicationId)
        {
            return await _interviewRepository.GetInterviewsByApplicationIdAsync(applicationId);
        }

        public async Task<Interview> GetInterviewByIdAsync(int interviewId)
        {
            return await _interviewRepository.GetInterviewByIdAsync(interviewId);
        }

        public async Task UpdateInterviewAsync(Interview interview)
        {
           
            await _interviewRepository.UpdateInterviewAsync(interview);
        }

        public async Task DeleteInterviewAsync(int interviewId)
        {
           
            await _interviewRepository.DeleteInterviewAsync(interviewId);
        }

        public async Task<List<Interview>> GetScheduledInterviewsForEmployerAsync(int employerId)
        {

           return await _interviewRepository.GetScheduledInterviewsForEmployerAsync(employerId);

        }
        public async Task<bool> HasConflictAsync(Interview newInterview)
        {
            var conflictingInterviews = await _dbContext.Interviews
                .Where(i =>
                    (i.Time >= newInterview.Time && i.Time <= newInterview.Time.Value.AddMinutes(45)) ||
                    (i.Time <= newInterview.Time && i.Time.Value.AddMinutes(45) >= newInterview.Time) ||
                    (i.Time <= newInterview.Time && i.Time.Value.AddMinutes(45) >= newInterview.Time.Value.AddMinutes(45)))
                .ToListAsync();

            // Check if there are any conflicting interviews
            return conflictingInterviews.Any();
        }

    }
}

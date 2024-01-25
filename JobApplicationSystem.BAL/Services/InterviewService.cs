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
        private readonly IJobRepository<Job> _jobRepository;
        private readonly JobApplicationSystemContext _context;

        public InterviewService(IInterviewRepository<Interview> interviewRepository, IJobRepository<Job> jobRepository, JobApplicationSystemContext context)
        {
            _interviewRepository = interviewRepository;
            _jobRepository = jobRepository;
            _context = context; 
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
            var conflictingInterviews = await _context.Interviews
                .AnyAsync(i => i.Time == newInterview.Time && i.InterviewId != newInterview.InterviewId);

            bool isScheduledBeforeToday = newInterview.Time.HasValue && newInterview.Time < DateTime.Today;

            return conflictingInterviews || isScheduledBeforeToday;
        }




    }
}
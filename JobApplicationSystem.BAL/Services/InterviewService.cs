using System.Collections.Generic;
using System.Threading.Tasks;
using JobApplicationSystem.DAL.Contracts;
using JobApplicationSystem.DAL.Models;
using JobApplicationSystem.DAL.Repositories;

namespace JobApplicationSystem.BAL.Services
{
    public class InterviewService : IInterviewService
    {
        private readonly IInterviewRepository<Interview> _interviewRepository;
        private readonly IJobRepository<Job> _jobRepository;

        public InterviewService(IInterviewRepository<Interview> interviewRepository, IJobRepository<Job> jobRepository)
        {
            _interviewRepository = interviewRepository;
            _jobRepository = jobRepository;
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
    }
}

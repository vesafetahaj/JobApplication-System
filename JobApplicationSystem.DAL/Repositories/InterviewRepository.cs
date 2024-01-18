﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JobApplicationSystem.DAL.Models;
using JobApplicationSystem.DAL.Data;

namespace JobApplicationSystem.DAL.Repositories
{
    public class InterviewRepository : IInterviewRepository<Interview>
    {
        private readonly JobApplicationSystemContext _context;

        public InterviewRepository(JobApplicationSystemContext context)
        {
            _context = context;
        }

        public async Task ScheduleInterviewAsync(Interview interview)
        {
            _context.Interviews.Add(interview);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Interview>> GetInterviewsByApplicationIdAsync(int applicationId)
        {
            return await _context.Interviews
                .Where(i => i.Application == applicationId)
                .ToListAsync();
        }

        public async Task<Interview> GetInterviewByIdAsync(int interviewId)
        {
            return await _context.Interviews
                .FirstOrDefaultAsync(i => i.InterviewId == interviewId);
        }

        public async Task UpdateInterviewAsync(Interview interview)
        {
            _context.Interviews.Update(interview);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInterviewAsync(int interviewId)
        {
            var interview = await GetInterviewByIdAsync(interviewId);
            if (interview != null)
            {
                _context.Interviews.Remove(interview);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Interview>> GetScheduledInterviewsForEmployerAsync(int employerId)
        {

            var interviews = await _context.Interviews
                               .Include(i => i.ApplicationNavigation)
                                .ThenInclude(app => app.JobNavigation)
                                .ThenInclude(job => job.EmployerNavigation)
                                .Include(i => i.ApplicationNavigation)
                                .ThenInclude(app => app.ApplicantNavigation)
                                .Where(i => i.ApplicationNavigation.JobNavigation.Employer == employerId)
                                .ToListAsync();

            return interviews;
        }
    }
}
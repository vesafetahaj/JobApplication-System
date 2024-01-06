using JobApplicationSystem.DAL.Contracts;
using JobApplicationSystem.DAL.Data;
using JobApplicationSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationSystem.DAL.Repositories
{
    public class ApplicantRepository : IApplicantRepository<Applicant>
    {
        private readonly JobApplicationSystemContext _dbContext;

        public ApplicantRepository(JobApplicationSystemContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Applicant> GetApplicantAsync(string userId)
        {
            return await _dbContext.Applicants
                .Include(e => e.UserNavigation)
                .FirstOrDefaultAsync(m => m.User == userId);
        }
        public async Task<bool> SaveApplicantAsync(Applicant applicant)
        {
            try
            {
                _dbContext.Add(applicant);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<Applicant> GetApplicantByIdAsync(int applicantId)
        {
            return await _dbContext.Applicants
                .FirstOrDefaultAsync(a => a.ApplicantId == applicantId);
        }
        public bool HasProvidedPersonalInfo(string userId)
        {
            return _dbContext.Applicants.Any(e => e.User == userId && e.Name != null && e.Surname != null);
        }
        public async Task<bool> EditPersonalInfoAsync(Applicant applicant)
        {
            try
            {
                _dbContext.Entry(applicant).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
       

    }
}

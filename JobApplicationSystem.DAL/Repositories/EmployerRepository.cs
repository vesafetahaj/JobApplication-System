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
    public class EmployerRepository : IEmployerRepository<Employer>
    {
        private readonly JobApplicationSystemContext _dbContext;

        public EmployerRepository(JobApplicationSystemContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Employer> GetEmployerAsync(string userId)
        {
            return await _dbContext.Employers
                .Include(e => e.UserNavigation)
                .FirstOrDefaultAsync(m => m.User == userId);
        }
        public async Task<bool> SaveEmployerAsync(Employer employer)
        {
            try
            {
                _dbContext.Add(employer);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<Employer> GetEmployerByIdAsync(int employerId)
        {
            return await _dbContext.Employers
                .FirstOrDefaultAsync(e => e.EmployerId == employerId);
        }
        public bool HasProvidedPersonalInfo(string userId)
        {
            return _dbContext.Employers.Any(e => e.User == userId && e.Name != null && e.Surname != null);
        }
        public async Task<bool> EditPersonalInfoAsync(Employer employer)
        {
            try
            {
                _dbContext.Entry(employer).State = EntityState.Modified;
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

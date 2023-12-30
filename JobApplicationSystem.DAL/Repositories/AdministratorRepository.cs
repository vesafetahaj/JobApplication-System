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
    public class AdministratorRepository : IAdministratorRepository<Administrator>
    {
        private readonly JobApplicationSystemContext _dbContext;

        public AdministratorRepository(JobApplicationSystemContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Administrator> GetAdministratorAsync(string userId)
        {
            return await _dbContext.Administrators
                .Include(e => e.UserNavigation)
                .FirstOrDefaultAsync(m => m.User == userId);
        }
        public async Task<bool> SaveAdministratorAsync(Administrator admin)
        {
            try
            {
                _dbContext.Add(admin);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<Administrator> GetAdministratorByIdAsync(int adminId)
        {
            return await _dbContext.Administrators
                .FirstOrDefaultAsync(a => a.AdminId == adminId);
        }
        public bool HasProvidedPersonalInfo(string userId)
        {
            return _dbContext.Administrators.Any(e => e.User == userId && e.Name != null && e.Surname != null);
        }
        public async Task<bool> EditPersonalInfoAsync(Administrator admin)
        {
            try
            {
                _dbContext.Entry(admin).State = EntityState.Modified;
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

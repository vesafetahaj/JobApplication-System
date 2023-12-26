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
    public class CompanyRepository: ICompanyRepository<Company>
    {
        private readonly JobApplicationSystemContext _dbContext;

        public CompanyRepository(JobApplicationSystemContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Company>> GetCompaniesAsync()
        {
            return await _dbContext.Companies.ToListAsync();
        }
    }
}

using JobApplicationSystem.DAL.Contracts;
using JobApplicationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationSystem.BAL.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository<Company> _companyRepository;

        public CompanyService(ICompanyRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<List<Company>> GetCompaniesAsync()
        {
            return await _companyRepository.GetCompaniesAsync();
        }
    }
}

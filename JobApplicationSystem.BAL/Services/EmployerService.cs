using JobApplicationSystem.DAL.Contracts;
using JobApplicationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationSystem.BAL.Services
{
    public class EmployerService : IEmployerService
    {
        private readonly IEmployerRepository<Employer> _employerRepository;

        public EmployerService(IEmployerRepository<Employer> employerRepository)
        {
            _employerRepository = employerRepository;
        }

        public async Task<Employer> GetEmployerDetailsAsync(string userId)
        {
            return await _employerRepository.GetEmployerAsync(userId);
        }

        public bool HasProvidedPersonalInfo(string userId)
        {
            return _employerRepository.HasProvidedPersonalInfo(userId);
        }
        public async Task<Employer> GetEmployerByIdAsync(int employerId)
        {
            return await _employerRepository.GetEmployerByIdAsync(employerId);
        }
        public async Task<bool> SavePersonalInfoAsync(Employer employer)
        {
            return await _employerRepository.SaveEmployerAsync(employer);
        }

        public async Task<bool> EditPersonalInfoAsync(Employer employer)
        {
            return await _employerRepository.EditPersonalInfoAsync(employer);
        }
    }
}

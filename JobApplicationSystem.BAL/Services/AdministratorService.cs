using JobApplicationSystem.DAL.Contracts;
using JobApplicationSystem.DAL.Models;
using JobApplicationSystem.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationSystem.BAL.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly IAdministratorRepository<Administrator> _adminRepository;

        public AdministratorService(IAdministratorRepository<Administrator> adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public async Task<Administrator> GetAdministratorDetailsAsync(string userId)
        {
            return await _adminRepository.GetAdministratorAsync(userId);
        }

        public bool HasProvidedPersonalInfo(string userId)
        {
            return _adminRepository.HasProvidedPersonalInfo(userId);
        }
        public async Task<Administrator> GetAdministratorByIdAsync(int adminId)
        {
            return await _adminRepository.GetAdministratorByIdAsync(adminId);
        }
        public async Task<bool> SavePersonalInfoAsyncAdmin(Administrator admin)
        {
            return await _adminRepository.SaveAdministratorAsync(admin);
        }
        public async Task<bool> EditPersonalInfoAsync(Administrator admin)
        {
            return await _adminRepository.EditPersonalInfoAsync(admin);
        }
    }
}

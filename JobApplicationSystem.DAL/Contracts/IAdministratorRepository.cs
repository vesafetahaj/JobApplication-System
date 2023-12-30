using JobApplicationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationSystem.DAL.Contracts
{
    public interface IAdministratorRepository<Administrator>
    {
        Task<Administrator> GetAdministratorAsync(string userId);
        Task<bool> SaveAdministratorAsync(Administrator admin);
        bool HasProvidedPersonalInfo(string userId);
        Task<Administrator> GetAdministratorByIdAsync(int admin);
        Task<bool> EditPersonalInfoAsync(Administrator admin);
    }
}

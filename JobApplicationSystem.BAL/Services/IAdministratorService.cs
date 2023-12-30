using JobApplicationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationSystem.BAL.Services
{
    public interface IAdministratorService
    {
        Task<Administrator> GetAdministratorDetailsAsync(string userId);
        Task<bool> SavePersonalInfoAsyncAdmin(Administrator admin);
        bool HasProvidedPersonalInfo(string userId);
        Task<Administrator> GetAdministratorByIdAsync(int adminId);
        Task<bool> EditPersonalInfoAsync(Administrator admin);

    }
}

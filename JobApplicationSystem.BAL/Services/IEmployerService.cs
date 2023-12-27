using JobApplicationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationSystem.BAL.Services
{
    public interface IEmployerService
    {
        Task<Employer> GetEmployerDetailsAsync(string userId);
        Task<bool> SavePersonalInfoAsync(Employer employer);
        bool HasProvidedPersonalInfo(string userId);
        Task<Employer> GetEmployerByIdAsync(int employerId);
        Task<bool> EditPersonalInfoAsync(Employer employer);
    }
}

using JobApplicationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationSystem.DAL.Contracts
{
    public interface IEmployerRepository<Employer>
    {
        Task<Employer> GetEmployerAsync(string userId);
        Task<bool> SaveEmployerAsync(Employer employer);
        bool HasProvidedPersonalInfo(string userId);
        Task<Employer> GetEmployerByIdAsync(int employerId);
        Task<bool> EditPersonalInfoAsync(Employer employer);
    }
}

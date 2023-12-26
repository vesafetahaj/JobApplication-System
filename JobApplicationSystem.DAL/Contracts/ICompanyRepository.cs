using JobApplicationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationSystem.DAL.Contracts
{
    public interface ICompanyRepository<Company>
    {
        Task<List<Company>> GetCompaniesAsync();
    }
}

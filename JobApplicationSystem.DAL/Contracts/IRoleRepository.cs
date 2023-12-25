using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobApplicationSystem.Models;

namespace JobApplicationSystem.DAL.Contracts
{
    public interface IRoleRepository<AspNetRole>
    {
        List<AspNetRole> GetAll();
    }
}

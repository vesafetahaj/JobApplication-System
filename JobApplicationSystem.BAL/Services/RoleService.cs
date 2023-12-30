using JobApplicationSystem.DAL.Contracts;
using JobApplicationSystem.DAL.Repositories;
using JobApplicationSystem.DAL.Data;
using JobApplicationSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JobApplicationSystem.BAL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository<AspNetRole> _rolerepository;
        public RoleService(IRoleRepository<AspNetRole> rolerepository)
        {
            _rolerepository = rolerepository;
        }
        public List<AspNetRole> GetAllRoles()
        {
            return _rolerepository.GetAll();
        }

        
    }
}

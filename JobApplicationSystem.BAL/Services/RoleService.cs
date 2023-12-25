using JobApplicationSystem.DAL.Contracts;
using JobApplicationSystem.DAL.Repositories;
using JobApplicationSystem.Data;
using JobApplicationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JobApplicationSystem.BAL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository<AspNetRole> _repository;
        public RoleService(IRoleRepository<AspNetRole> _repository)
        {
            _repository = _repository;
        }
        public List<AspNetRole> GetAllRoles()
        {
            return _repository.GetAll();
        }

        
    }
}

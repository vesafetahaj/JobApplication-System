using JobApplicationSystem.DAL.Contracts;
using JobApplicationSystem.DAL.Data;
using JobApplicationSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationSystem.DAL.Repositories
{
    public class RoleRepository : IRoleRepository<AspNetRole>
    {
        private readonly JobApplicationSystemContext _dbContext;

        public RoleRepository(JobApplicationSystemContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<AspNetRole> GetAll()
        {
            return _dbContext.AspNetRoles.ToList();
        }
    }
}

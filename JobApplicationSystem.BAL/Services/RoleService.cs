using JobApplicationSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JobApplicationSystem.BAL.Services
{
    public class RoleService : IRoleService
    {
        private readonly JobApplicationSystemContext _dbContext;
        public RoleService(JobApplicationSystemContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<string> GetAllRoles()
        {
            return _dbContext.AspNetRoles
            .Select(role => role.Name)
            .ToList();
        }

        
    }
}

using Demo.BLL.Interface;
using Dome.DAL.Data;
using Dome.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositores
{
    public class IDepartmnetRepository : GeneicRopository<Department>, IDepartmentRepository
    {
        private readonly AppDbContext _dbContext;

        public IDepartmnetRepository(AppDbContext dbComtext ):base(dbComtext)    // Ask CLR For Create Object frpm DbContext
        {
            // _dbContext = new AppDbContext();

            // _dbContext = dbComtext;   // DJ
           
        }

        public IQueryable<Department> SearchByName(string name)
        {
           return _dbContext.Departments.Where(D=>D.Name.ToLower().Contains(name.ToLower()));
        }
    }
}

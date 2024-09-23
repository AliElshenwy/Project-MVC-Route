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
    public class EmployeeRepository : GeneicRopository<Employee>, IEmployeeRepository
    {
        private readonly AppDbContext _DbContext;

        public EmployeeRepository(AppDbContext dbContext):base(dbContext)// Ask Clr to Create object
        {
           // _DbContext = dbContext;
        }
       
        public IQueryable<Employee> GetEmployeeByAddress(string address)
        {
           return _DbContext.Employees.Where(E=>E.Address.ToLower().Contains(address.ToLower()));   
        }
     

        public IQueryable<Employee>SearchByName(string name)
        {
            return _DbContext.Employees.Where(E => E.Name.ToLower().Contains(name.ToLower()));
        }
    }
}

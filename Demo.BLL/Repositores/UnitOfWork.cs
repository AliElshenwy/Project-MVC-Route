using Demo.BLL.Interface;
using Dome.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositores
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
           EmployeeRepository=new EmployeeRepository(_dbContext);
            DepartmentRepository =new IDepartmnetRepository(_dbContext);

        }
        public IEmployeeRepository EmployeeRepository { get ; set; }
        public IDepartmentRepository DepartmentRepository { get ; set ; }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }
        public void Dispose() 
        { 
            _dbContext.Dispose();
        }
    }
}

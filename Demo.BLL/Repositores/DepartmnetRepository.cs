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
    public class DepartmnetRepository : IDepartmentRepository
    {
        private readonly AppDbContext _dbContext;

        public DepartmnetRepository(AppDbContext dbComtext )    // Ask CLR For Create Object frpm DbContext
        {
           // _dbContext = new AppDbContext();
           // 
           _dbContext = dbComtext;   // DJ
        }
        public int Add(Department department)
        {
           _dbContext.Departments.Add(department);  //State Add
            return _dbContext.SaveChanges();    
        }

        public int Delete(Department department)
        {
            _dbContext.Departments.Remove(department);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Department> GetAll() 
        { 
           return _dbContext.Departments.AsNoTracking().ToList();
        }

        public Department GetById(int id)
        {
            return _dbContext.Departments.Find(id);
            //return _dbContext.Find<Department>(id); 
            
        }

        public int Update(Department department)
        {
            _dbContext.Departments.Update(department);
            return _dbContext.SaveChanges();    
        }
    }
}

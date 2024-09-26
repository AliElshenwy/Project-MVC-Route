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
    public class GeneicRopository<T> : GenericRepositort<T> where T : ModelBase
    {
        private readonly AppDbContext _DbContext;
        public GeneicRopository(AppDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public void Add(T item)
        {
           // _DbContext.Set<T>().Add(item);  // State Add 
            _DbContext.Add(item);
           // return _DbContext.SaveChanges();

        }
        public void Delete(T item)
        {
            _DbContext.Remove(item); // State Remove (Deleted)
            //return _DbContext.SaveChanges();
        }
        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return(IEnumerable<T>) _DbContext.Set<Employee>().Include(E =>E.department).AsNoTracking().ToList();
            }
            else
            {
                return _DbContext.Set<T>().AsNoTracking().ToList();
            }
          
        }

        public T GetById(int id)
        {
            return _DbContext.Find<T>(id);
        }

       

        public void Update(T item)
        {
            _DbContext.Update(item);
            //return _DbContext.SaveChanges();
        }
    }
}

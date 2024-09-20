using Dome.DAL.Data.Configuration;
using Dome.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dome.DAL.Data
{
 public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)      
        {
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //MuilteActiveResultSets=True    Use For Req Alot Of Query 
        //    optionsBuilder.
        //      UseSqlServer("Server =. ;Database=ProjectMVC; Trusted-Connection=True; MuilteActiveResultSets=True ");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent Api
           // modelBuilder.ApplyConfiguration<Department>(new DepartmentConfigurations() ); //one Configuration

            modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());  // All Configuration 
            base.OnModelCreating(modelBuilder);
        }
       public DbSet<Department>Departments { get; set; }
       public DbSet<Employee> Employees { get; set; }
    }
}

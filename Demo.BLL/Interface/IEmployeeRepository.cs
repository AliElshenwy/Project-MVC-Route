using Dome.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interface
{
    public interface IEmployeeRepository :GenericRepositort<Employee>
    {
     
        // GetEmployeeDyAddress
        IQueryable<Employee>GetEmployeeByAddress(string address);
    }
}

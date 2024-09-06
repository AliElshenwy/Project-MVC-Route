using Dome.DAL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interface 
{
   public interface IDepartmentRepository 
    { 
        IEnumerable<Department>GetAll();
        Department GetById(int id);
        int Add (Department department);
        int Update (Department department);
        int Delete (Department department);
    }
}

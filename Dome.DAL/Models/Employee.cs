using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dome.DAL.Models
{

    public enum Gender
    {
        [EnumMember(Value = "Male")]
        Male = 1,
        [EnumMember(Value = "Female")]
        Female = 2
    }
    public enum EmployeeType
    {
        FullTime = 1,
        PartTime = 2
    }
    public class Employee : ModelBase
    {

        [Required()]
        [MaxLength(50)]
     
        public string Name { get; set; }
        [Range(21, 60)]
        public int? Age { get; set; }
       public string Address { get; set; }


        public decimal Salary { get; set; }


        public bool IsActive { get; set; }

        public string Email { get; set; }


       
        public int PhoneNumber { get; set; }


        public DateTime HireDate { get; set; }

        public bool IsDeleted { get; set; } // soft Delete


        public Gender Gender { get; set; }

        //Navgation property [one]
       // [InverseProperty(nameof(Models.Department.employees))]
        public Department department { get; set; }
        public int? DepartmentId { get; set; }


    }
}

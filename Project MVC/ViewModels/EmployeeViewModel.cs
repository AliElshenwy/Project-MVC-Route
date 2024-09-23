using Dome.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System;

namespace Project_MVC.ViewModels
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
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required!")]
            [MaxLength(50, ErrorMessage = "Max Lengthfor Name Is 50 ")]
            [MinLength(3, ErrorMessage = "Min Lengthfor Name Is 3 ")]
            public string Name { get; set; }


            [Range(21, 60)]
            public int? Age { get; set; }


            [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
                ErrorMessage = " Address Must Be Like 123-Street-City-Country")]
            public string Address { get; set; }


            [DataType(DataType.Currency)]
            public decimal Salary { get; set; }


            public bool IsActive { get; set; }


            [EmailAddress]
            public string Email { get; set; }


            [Phone]
            [Display(Name = "Phone Number")]
            public int PhoneNumber { get; set; }


            [Display(Name = "Hire Date")]
            public DateTime HireDate { get; set; }

            public bool IsDeleted { get; set; } // soft Delete


            public Gender Gender { get; set; }

            //Navgation property [one]

            //[InverseProperty(nameof(Models.Department.employees))]
            public Department department { get; set; }
            public int? DepartmentId { get; set; }
        }
    }

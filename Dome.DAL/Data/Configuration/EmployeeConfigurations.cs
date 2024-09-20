using Dome.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dome.DAL.Data.Configuration
{
    internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Salary)
                 .HasColumnType("decimal(18,2)");


            //تحويل قيمه النوع الي رقم في الداته بيز
            // Fluent API'S   
            builder.Property(E=> E.Gender)
                .HasConversion((Gender)=>Gender.ToString(),
                (genderAsString)=>(Gender)Enum.Parse(typeof(Gender),genderAsString) 
                );
        }
    }
}

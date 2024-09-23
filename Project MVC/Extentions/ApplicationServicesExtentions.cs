using Demo.BLL.Interface;
using Demo.BLL.Repositores;
using Microsoft.Extensions.DependencyInjection;

namespace Project_MVC.Extensions
{
    public static class ApplicationServicesExtentions
    {

        public static IServiceCollection  AddApplictionServices( this IServiceCollection services)
        {
            services.AddScoped<IDepartmentRepository, IDepartmnetRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            return services;
        }
    }
}

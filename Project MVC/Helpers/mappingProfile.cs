using AutoMapper;
using Dome.DAL.Models;
using Project_MVC.ViewModels;

namespace Project_MVC.Helpers
{
    public class mappingProfiles :Profile
    {
        public mappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}

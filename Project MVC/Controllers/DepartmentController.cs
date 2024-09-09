using Demo.BLL.Interface;
using Demo.BLL.Repositores;
using Microsoft.AspNetCore.Mvc;

namespace Project_MVC.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmnetRepository;  //Default Null ,//Call DepartmnentRepository 
        public DepartmentController(IDepartmentRepository repository)
        {
            _departmnetRepository = repository;
        }


        // BaseUrl/department/Index
        public IActionResult Index()
        {
            // GetAll()
           var departments =_departmnetRepository.GetAll();
            
            return View(departments);
        }
    }
}

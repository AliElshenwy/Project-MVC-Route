using Demo.BLL.Repositores;
using Microsoft.AspNetCore.Mvc;

namespace Project_MVC.Controllers
{
    public class DepartmentController : Controller
    {
        private DepartmnetRepository _departmnetRepository;
        public IActionResult Index()
        {
            // GetAll()
            _departmnetRepository.GetAll();
            
            return View();
        }
    }
}

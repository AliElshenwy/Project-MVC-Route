using Demo.BLL.Interface;
using Demo.BLL.Repositores;
using Dome.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
                [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
               var Count = _departmnetRepository.Add(department);
                    if(Count > 0 )
                    {
                       return RedirectToAction(nameof(Index));
                    }
                   
            }
            return View(department);
        }
         

        //Department/Details/ Id
        [HttpGet]
        public IActionResult Details(int? id)  // (?) Nullable  =>  HAsValue check Value
        {
           if(id.HasValue)
            {
                return BadRequest();
            }  
           var department = _departmnetRepository.GetById(id.Value); // Chech Date
            if (department == null)
            {
                return NotFound();  //Error 404
            }
            return View(department);
        }
    }
}

using Demo.BLL.Interface;
using Demo.BLL.Repositores;
using Dome.DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;

namespace Project_MVC.Controllers
{
    public class EmployeeController: Controller
    {
        private readonly IEmployeeRepository _EmployeeRepository;  //Default Null ,//Call DepartmnentRepository 
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeRepository repository, IWebHostEnvironment env)
        {
            _EmployeeRepository = repository;
            _env = env;
        }


        // BaseUrl/Employee/Index
        public IActionResult Index()
        {
            // GetAll()
            var Employees = _EmployeeRepository.GetAll();

            //Binding Through View Dictionary :Transfer Data From Action to View
            //1.viewData 
            ViewData["Massage"] = "Hello ViewData";  // Key and Value 
            //2.ViewBag
            ViewBag.Message = "Hello ViewBag ";

            return View(Employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

      //  [HttpPost]
        public IActionResult Create(Employee Employee)
        {
            if (!ModelState.IsValid)
            {
                //3. TempData  ==> Action to Action 
                var Count = _EmployeeRepository.Add(Employee);
                if (Count > 0)
                {
                    TempData["Message"] = "Employee Created Succefully ";
                }
                else
                {
                    TempData["Message"] = "An Error Occurred";
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Employee);
        }

        //Employee/Details/ Id
        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")  // (?) Nullable  =>  HAsValue check Value
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var Employee = _EmployeeRepository.GetById(id.Value); // Chech Date
            if (Employee == null)
            {

                return NotFound();  //Error 404
            }
            return View(viewName, Employee);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken] // لعدم التعديل من toolخارجيه 
        //public IActionResult Edit([FromRoute] int id, Employee Employee)
        //{
        //    return Details(id, "Edit");
        //}


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var Employee = _EmployeeRepository.GetById(id.Value); // Chech Date
            if (Employee == null)
            {
                return NotFound();  //Error 404
            }
            return View(Employee);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, Employee Employee)
        {
            if (id != Employee.Id) //Check Id Url 
                return BadRequest();

            if (!ModelState.IsValid)
                return View(Employee);

            try
            {
                _EmployeeRepository.Update(Employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Ex)
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, Ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An Error occured During Update Employee ");
                }
                return View(Employee);
            }
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Employee Employee)
        {
            try
            {
                _EmployeeRepository.Delete(Employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Ex)
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, Ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An Error occured During Update Employee ");
                }
                return View(Employee);
            }
        }
    }
}

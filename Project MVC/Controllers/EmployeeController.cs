using AutoMapper;
using Demo.BLL.Interface;
using Demo.BLL.Repositores;
using Dome.DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Project_MVC.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Project_MVC.Controllers
{
    public class EmployeeController: Controller
    {
        private readonly IEmployeeRepository _EmployeeRepository;  //Default Null ,//Call DepartmnentRepository 
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        // private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IEmployeeRepository repository, IWebHostEnvironment env /*IDepartmentRepository departmentRepository*/ , IMapper mapper
            )
        {
            _EmployeeRepository = repository;
            _env = env;
            _mapper = mapper;
            // _departmentRepository = departmentRepository;
        }
        // BaseUrl/Employee/Index

       [HttpGet]
        public IActionResult Index(string searchInp)
        {
            if (string.IsNullOrEmpty(searchInp))
            {
                var Employees = _EmployeeRepository.GetAll();
                var mappedEmp = _mapper.Map< IEnumerable<Employee>, IEnumerable<EmployeeViewModel> > (Employees);
                return View(mappedEmp);
            }
            else
            {

                var Employees = _EmployeeRepository.SearchByName(searchInp.ToLower());
                var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employees);
                return View(mappedEmp);
            }

            // GetAll()
            // var Employees = _EmployeeRepository.GetAll();
            //Binding Through View Dictionary :Transfer Data From Action to View
            //1.viewData 
            //  ViewData["Massage"] = "Hello ViewData";  // Key and Value 
            //2.ViewBag
            // ViewBag.Message = "Hello ViewBag ";
            // return View(Employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
         //  ViewBag.Departments = _departmentRepository.GetAll();  
          
            return View();
        }

      //  [HttpPost]
        public IActionResult Create(EmployeeViewModel EmployeeVM)
        {
            if (!ModelState.IsValid)
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel ,Employee>(EmployeeVM);
                //3. TempData  ==> Action to Action 

                var Count = _EmployeeRepository.Add(mappedEmp);
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
            return View(EmployeeVM);
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
            //ViewBag.Departments = _departmentRepository.GetAll();
            if (Employee == null)
            {
                
                return NotFound();  //Error 404
            }
            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(Employee);
            return View(viewName, mappedEmp);
        }


     
        //[ValidateAntiForgeryToken] // لعدم التعديل من toolخارجيه 
        //public IActionResult Edit([FromRoute] int id, Employee Employee)
        //{
        //    return Details(id, "Edit");
        //}


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (!id.HasValue)
            //{
            //    return BadRequest();
            //}
            //var Employee = _EmployeeRepository.GetById(id.Value); // Chech Date

            //if (Employee == null)
            //{
            //    return NotFound();  //Error 404
            //}
            //return View(Employee);
            return Details(id, "Edit");
        }

        [HttpPost]
     
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel EmployeeVM)
        {
            if (id != EmployeeVM.Id) //Check Id Url 
                return BadRequest();

            if (!ModelState.IsValid)
                return View(EmployeeVM);

            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);
                _EmployeeRepository.Update(mappedEmp);
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
                return View(EmployeeVM);
            }
            
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(EmployeeViewModel EmployeeVM)
        {
            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);
                _EmployeeRepository.Delete(mappedEmp);
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
                return View(EmployeeVM);
            }
        }
    }
}

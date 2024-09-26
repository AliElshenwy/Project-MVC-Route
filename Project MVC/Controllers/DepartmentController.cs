using Demo.BLL.Interface;
using Demo.BLL.Repositores;
using Dome.DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using Project_MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace Project_MVC.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IDepartmentRepository _departmnetRepository;  //Default Null ,//Call DepartmnentRepository 
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IUnitOfWork unitOfWork ,IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            //_departmnetRepository = repository;
            _env = env;
        }

        [HttpGet]
        // BaseUrl/department/Index
        public IActionResult Index(string searchByName)
        {
            // GetAll()
            if (string.IsNullOrEmpty(searchByName))
            {
                var departments = _unitOfWork.DepartmentRepository.GetAll();
                // var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employees);
                  return View(departments);
            }
            else
            {
                var departments = _unitOfWork.DepartmentRepository.SearchByName(searchByName.ToLower()); //?? string.Empty);
                // var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employees);
                return View(departments);
            }
            //return View(departments);
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
              _unitOfWork.DepartmentRepository.Add(department);
             var Count= _unitOfWork.Complete();
               if(Count > 0 )
                 {
                   return RedirectToAction(nameof(Index));
                 }
                   
            }
            return View(department);
        }
         

        //Department/Details/ Id
        [HttpGet]
        public IActionResult Details(int? id , string viewName="Details")  // (?) Nullable  =>  HAsValue check Value
        {
           if(!id.HasValue)
            {
                return BadRequest();
            }  
           var department = _unitOfWork.DepartmentRepository.GetById(id.Value); // Chech Date
            if (department == null)
            {

                return NotFound();  //Error 404
            }
            return View( viewName,department);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken] // لعدم التعديل من toolخارجيه 
        //public IActionResult Edit([FromRoute] int id, Department department)
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
            var department = _unitOfWork.DepartmentRepository.GetById(id.Value); // Chech Date
            if (department == null)
            {
                return NotFound();  //Error 404
            }
            return View(department);
        }

        [HttpPost]
        public IActionResult Edit( [FromRoute]int id ,Department department)
        {
            if (id != department.Id) //Check Id Url 
                return BadRequest();

            if (!ModelState.IsValid)
                return View(department);
            
            try
            {
                _unitOfWork.DepartmentRepository.Update(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Ex)
            {
                if(_env.IsDevelopment())
                {
                   ModelState.AddModelError(string.Empty,Ex.Message); 
                }
                else
                {
                    ModelState.AddModelError(string.Empty,"An Error occured During Update Department ");
                }
                return View(department);    
            }
        }
        [HttpGet]
        public IActionResult Delete(int ? id)
        {
            return Details(id,"Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Department department)
        {
            try
            {
                _unitOfWork.DepartmentRepository.Delete(department);
                _unitOfWork.Complete();
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
                    ModelState.AddModelError(string.Empty, "An Error occured During Update Department ");
                }
                return View(department);
            }
        }
    }
}

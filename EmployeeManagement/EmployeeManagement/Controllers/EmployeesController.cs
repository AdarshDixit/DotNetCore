using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Model;
using EmployeeManagement.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("Employees")]
    public class EmployeesController : Controller
    {
        private IEmployeeRepository employeeRepository;
        private readonly IWebHostEnvironment hostingEnvironment;       

        public EmployeesController(IEmployeeRepository employeeRepository, IWebHostEnvironment hostingEnvironment)
        {
            this.employeeRepository = employeeRepository;
            this.hostingEnvironment = hostingEnvironment;
        } 
        [Route("")]
        [Route("~/")]
        [Route("GetEmployees")]
        public ViewResult GetEmployees()
        {
            var model = new EmployeesViewModel
            {
                Employees = employeeRepository.GetEmployees()
            };
            return View(model);
        }

        [Route("GetEmployee/{id}")]
        public ViewResult GetEmployee(int? id)
        {
            if (!id.HasValue)
            {
                return View(new EmployeeViewModel { ErrorMessage = "Please provide an Id"});
            }
            var model = new EmployeeViewModel
            {
                Employee = employeeRepository.GetEmployee(id.Value)
            };
            return View(model);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(CreateEmployeeViewModel model)
        {

            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(this.hostingEnvironment.WebRootPath, "images");                  
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                   
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Designation = model.Designation,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };
                var emp = employeeRepository.Add(newEmployee);
                return RedirectToAction("GetEmployee", "Employees", new { id = emp.Id });
            }
            return View();
        }
    }
}
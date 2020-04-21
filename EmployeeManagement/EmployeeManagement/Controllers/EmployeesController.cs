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
        [Route("Details")]
        public ViewResult GetEmployees()
        {
            var model = new EmployeesViewModel
            {
                Employees = employeeRepository.GetEmployees()
            };
            return View(model);
        }

        [Route("Details/{id}")]
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
                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Designation = model.Designation,
                    Department = model.Department,
                    PhotoPath = ProcessUploadedFile(model)
                };
                var emp = employeeRepository.Add(newEmployee);
                return RedirectToAction("GetEmployee", "Employees", new { id = emp.Id });
            }
            return View();
        }

        [HttpGet]
        [Route("Edit")]
        public IActionResult Edit(int id)
        {
            var emp = employeeRepository.GetEmployee(id);
            var model = new EditEmployeeViewModel
            {
                Id = emp.Id,
                Name = emp.Name,
                Department = emp.Department,
                Designation = emp.Designation,
                ExistingPhotoPath = emp.PhotoPath
            };
            return View(model);
        }

        [HttpPost]
        [Route("Edit")]
        public IActionResult Edit(EditEmployeeViewModel model)
        {

            if (ModelState.IsValid)
            {
                Employee employee = employeeRepository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Designation = model.Designation;
                employee.Department = model.Department;

                if (model.Photo != null)
                {                  
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath,
                            "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    
                    employee.PhotoPath = ProcessUploadedFile(model);
                }               
                Employee updatedEmployee = employeeRepository.Update(employee);

                return RedirectToAction("GetEmployees");
            }
            return View();
        }

        private string ProcessUploadedFile(CreateEmployeeViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
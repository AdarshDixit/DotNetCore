using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Model;
using EmployeeManagement.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("Employees")]
    public class EmployeesController : Controller
    {
        private IEmployeeRepository employeeRepository;
        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
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
        public IActionResult Create(Employee employee)
        {

            if (ModelState.IsValid)
            {
                var emp = employeeRepository.Add(employee);
                return RedirectToAction("GetEmployee", "Employees", new { id = emp.Id });
            }
            return View();
        }
    }
}
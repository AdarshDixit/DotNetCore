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
    }
}
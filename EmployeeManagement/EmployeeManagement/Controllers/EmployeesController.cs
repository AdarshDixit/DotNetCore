using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Model;
using EmployeeManagement.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class EmployeesController : Controller
    {
        private IEmployeeRepository employeeRepository;
        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        } 
        [Route("")]
        [Route("Employees/AllEmployees")]
        public ViewResult AllEmployees()
        {
            var model = new EmployeesViewModel
            {
                Employees = employeeRepository.GetEmployees()
            };
            return View(model);
        }
    }
}
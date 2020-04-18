using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Model;
using EmployeeManagement.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private IEmployeeRepository repo;

        public AccountController(IEmployeeRepository repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(Employee employee)
        {

            if (ModelState.IsValid)
            {
                var emp = repo.Add(employee);
                return RedirectToAction("GetEmployee", "Employees", new { id = emp.Id }); 
            }
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("Error")]
    public class ErrorController : Controller
    {
        [Route("{0}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            ViewBag.Error = "Some Error Occured";
            return View();
        }
    }
}
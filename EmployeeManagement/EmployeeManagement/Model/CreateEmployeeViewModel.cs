using EmployeeManagement.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public class CreateEmployeeViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        public string Designation { get; set; }

        // Add nullable enum for validation
        [Required(ErrorMessage = "Please select a value")]
        public Department? Department { get; set; }

        public IFormFile Photo { get; set; }
    }
}

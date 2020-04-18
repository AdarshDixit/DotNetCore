using EmployeeManagement.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        public string Designation { get; set; }

        // Add nullable enum for validation
        [Required(ErrorMessage="Please select a value")]
        public Department? Department { get; set; }
    }
}

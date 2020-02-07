using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public class EmployeesViewModel
    {
        public List<Employee> Employees { get; set; }
        public int PageNumber { get; set; }
    }
}

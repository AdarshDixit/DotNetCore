using EmployeeManagement.Enums;
using EmployeeManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Repository
{
    public class EmployeeRepository
    {
        private List<Employee> employees = new List<Employee>
        {
            new Employee
            {
                Id = 1,Name = "Adarsh Dixit", Department = Department.Finance, Designation = "Accountant"
            },
             new Employee
            {
                Id = 2,Name = "Aman Dixit", Department = Department.Operations, Designation = "Developer"
            },
              new Employee
            {
                Id = 3,Name = "Madhuri Dixit", Department = Department.HR, Designation = "Manager"
            },
        };

        public Employee Add(Employee employee)
        {
            employee.Id = employees.Max(x => x.Id) + 1;
            employees.Add(employee);
            return employee;
        }

        public Employee GetEmployee(int id)
        {
            return employees.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<Employee> GetEmployees()
        {
            return employees;
        }
    }
}

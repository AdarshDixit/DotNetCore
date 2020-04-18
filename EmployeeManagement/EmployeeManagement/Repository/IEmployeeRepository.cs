using EmployeeManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Repository
{
    public interface IEmployeeRepository
    {
        public Employee GetEmployee(int id);
        public List<Employee> GetEmployees();
        public Employee Add(Employee employee);
        public Employee Delete(Employee employee);
        public Employee Update(Employee employee);
    }
}

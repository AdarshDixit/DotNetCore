using EmployeeManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Repository
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private AppDbContext context;

        public SQLEmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Employee Add(Employee employee)
        {
            this.context.Employees.Add(employee);
            this.context.SaveChanges();
            return employee;
        }

        public Employee Delete(Employee employee)
        {
            var emp = this.context.Employees.Find(employee);
            if (emp != null)
            {
                this.context.Employees.Remove(emp);
                this.context.SaveChanges();
            }
            return employee;
        }

        public Employee GetEmployee(int id)
        {
            return this.context.Employees.Find(id);
        }

        public List<Employee> GetEmployees()
        {
            return this.context.Employees.ToList();
        }

        public Employee Update(Employee employee)
        {
            var emp = this.context.Attach(employee);
            emp.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return employee;
        }
    }
}

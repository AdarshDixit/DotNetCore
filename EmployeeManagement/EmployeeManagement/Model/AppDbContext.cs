using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                { Id = 1, Name = "Adarsh Dixit", Department = Enums.Department.Finance, Designation = "Sr. Manager"},
                new Employee
                { Id = 2, Name = "Aman Dixit", Department = Enums.Department.IT, Designation = "Software Architect" },
                new Employee
                { Id = 3, Name = "Hutiya Insaan", Department = Enums.Department.HR, Designation = "Manager" }            
                );
        }
    }
}

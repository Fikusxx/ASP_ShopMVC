using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Test_ASP_Project.Models.Enums;

namespace Test_ASP_Project.Models.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee() { Id = 1, Name = "Tom", Email = "tom@gmail.com", Department = Departments.IT },
                new Employee() { Id = 2, Name = "Alice", Email = "alice@gmail.com", Department = Departments.Sales },
                new Employee() { Id = 3, Name = "Bob", Email = "bob@gmail.com", Department = Departments.Executive }
                );
        }
    }
}

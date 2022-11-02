using Microsoft.EntityFrameworkCore;

namespace Test_ASP_Project.Models
{
	public class SQLEmployeeRepository : IEmployeeRepository
	{
		private readonly ApplicationContext context;
		private readonly ILogger<SQLEmployeeRepository> logger;
		public SQLEmployeeRepository(ApplicationContext context, ILogger<SQLEmployeeRepository> logger)
		{
			this.context = context;
			this.logger = logger;
		}

		public Employee Add(Employee employee)
		{
			context.Employees.Add(employee);
			context.SaveChanges();
			return employee;
		}

		public Employee Delete(int id)
		{
			var emp = context.Employees.FirstOrDefault(x => x.Id == id);
			context.Employees.Remove(emp);
            context.SaveChanges();
            return emp;
        }

		public IEnumerable<Employee> GetAllEmployees()
		{
			return context.Employees;
		}

		public Employee? GetEmployeeById(int id)
		{
			var emp = context.Employees.FirstOrDefault(x => x.Id == id);
			return emp;
		}

		public Employee Update(Employee employeeChanges)
		{
            var employee = context.Employees.FirstOrDefault(x => x.Id == employeeChanges.Id);

			if (employee != null)
			{
				employee.Name = employeeChanges.Name;
				employee.Email = employeeChanges.Email;
				employee.Department = employeeChanges?.Department;
				context.Employees.Update(employee);
				context.SaveChanges();
			}

            return employee;
        }
	}
}

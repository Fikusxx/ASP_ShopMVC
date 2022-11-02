using Test_ASP_Project.Models.Enums;

namespace Test_ASP_Project.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private List<Employee> employees = new();
        public EmployeeRepository()
        {
            employees.Add(new Employee() { Id = 1, Name = "Tom", Email = "tom@gmail.com", Department = Departments.IT });
            employees.Add(new Employee() { Id = 2, Name = "Alice", Email = "alice@gmail.com", Department = Departments.Sales });
            employees.Add(new Employee() { Id = 3, Name = "Bob", Email = "bob@gmail.com", Department = Departments.Executive });
        }

        public Employee? GetEmployeeById(int id)
        {
            var employee = employees.FirstOrDefault(x => x.Id == id);
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return employees;
        }

        public Employee Add(Employee employee)
        {
            employee.Id = employees.Max(x => x.Id) + 1;
            employees.Add(employee);
            return employee;
        }

        public Employee Update(Employee employee)
        {
            var emp = employees.FirstOrDefault(x => x.Id == employee.Id);

            if (emp != null)
            {
                emp.Name = employee.Name;
                emp.Email = employee.Email;
                emp.Department = employee.Department;
            }

            return emp;
        }

        public Employee Delete(int id)
        {
            var emp = employees.FirstOrDefault(x => x.Id == id);
            employees.Remove(emp);
            return emp;
        }
    }
}

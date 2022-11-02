namespace Test_ASP_Project.Models
{
    public interface IEmployeeRepository
    {
        public Employee? GetEmployeeById(int id);
        public IEnumerable<Employee> GetAllEmployees();
        public Employee Add(Employee employee);
        public Employee Update(Employee employee);
        public Employee Delete(int id);
    }
}

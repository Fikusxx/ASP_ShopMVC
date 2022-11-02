using Test_ASP_Project.Models;



namespace Test_ASP_Project.ViewModels
{
    /// <summary>
    /// Home - HomeController
    /// Details - HomeController's method
    /// ViewModel - ViewModel
    /// </summary>
    public class HomeDetailsViewModel 
    {
        public Employee Employee { get; set; }
        public string PageTitle { get; set; }
        public HomeDetailsViewModel() { }
        public HomeDetailsViewModel(Employee employee, string pageTitle)
        {
            Employee = employee;
            PageTitle = pageTitle;
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Test_ASP_Project.Models;
using Test_ASP_Project.Secutity.Encryption;
using Test_ASP_Project.ViewModels;

namespace Test_ASP_Project.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly ILogger<HomeController> logger;
        private readonly IDataProtector dataProtector;
        public HomeController(IEmployeeRepository employeeRepository, IWebHostEnvironment hostingEnvironment, 
            ILogger<HomeController> logger, IDataProtectionProvider dataProtection,
            DataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            this.employeeRepository = employeeRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.logger = logger;
            this.dataProtector = dataProtection.CreateProtector(dataProtectionPurposeStrings.EmployeeIdRouteValue);
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var model = employeeRepository.GetAllEmployees()
                                          .Select(x =>
                                          {
                                              x.EncryptedId = dataProtector.Protect(x.Id.ToString());
                                              return x;
                                          });
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Details(string id)
        {
            int decryptedId = Convert.ToInt32(dataProtector.Unprotect(id));

            Employee? employee = employeeRepository.GetEmployeeById(decryptedId);

            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", decryptedId);
            }

            var homeDetailsViewModel = new HomeDetailsViewModel(employee, "Employee Details");

            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(HomeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);

                // создаем новый обект Employee на основе данных выше
                var newEmployee = new Employee()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };

                // добавляем обьект в БД
                employeeRepository.Add(newEmployee);

                // вызываем Details и показываем детали нового обьекта
                return RedirectToAction("Details", "Home", new { id = newEmployee.Id });
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = employeeRepository.GetEmployeeById(id);
            var viewModel = new HomeEditViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(HomeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = employeeRepository.GetEmployeeById(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;


                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        var filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }

                    employee.PhotoPath = ProcessUploadedFile(model);
                }

                employeeRepository.Update(employee);
                return RedirectToAction("details", "home", new { Id = employee.Id});
            }

            return View(model);
        }

        private string ProcessUploadedFile(HomeCreateViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photo != null)
            {
                // получаем путь до папки с картинками в wwwroot
                var uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                // получаем имя загружженого файла и соединяем его с guid
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                // получаем конечный путь/название для файла в строку
                var filePath = Path.Combine(uploadFolder, uniqueFileName);
                // создаем(копируем) файл(картинку) в папку wwwroot/images/imagename
                model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
            }

            return uniqueFileName;
        }
    } 
}

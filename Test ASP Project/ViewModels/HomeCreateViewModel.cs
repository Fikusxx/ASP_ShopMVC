using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Test_ASP_Project.Models.Enums;


namespace Test_ASP_Project.ViewModels
{
	public class HomeCreateViewModel
	{

        [Required]
        [MinLength(2, ErrorMessage = "Минимальная длина должна быть 2 символов")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Емейл")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Incorrent Email Format")]
        public string Email { get; set; }

        public Departments? Department { get; set; }
        public IFormFile? Photo { get; set; } 
    }
}

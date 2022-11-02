using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Test_ASP_Project.ViewModels
{
	public class PasswordViewModel
	{
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "Passwords dont match")]
        public string ConfirmPassword { get; set; }
    }
}

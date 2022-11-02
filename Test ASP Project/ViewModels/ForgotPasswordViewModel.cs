using System.ComponentModel.DataAnnotations;

namespace Test_ASP_Project.ViewModels
{
	public class ForgotPasswordViewModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}

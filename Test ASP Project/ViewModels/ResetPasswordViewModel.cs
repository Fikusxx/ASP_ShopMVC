using System.ComponentModel.DataAnnotations;


namespace Test_ASP_Project.ViewModels
{
	public class ResetPasswordViewModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

        [Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Passwords dont match")]
		[Display(Name = "Confirm Password")]
		public string ConfirmPassword { get; set; }
		
		public string Token { get; set; }
	}
}

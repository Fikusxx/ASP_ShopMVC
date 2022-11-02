using System.ComponentModel.DataAnnotations;
using Test_ASP_Project.Utilities;

namespace Test_ASP_Project.ViewModels
{
	public class EditUserViewModel
	{
		public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [ValidEmailDomain(allowedDomain: "test.com", ErrorMessage = "Email is not in test.com domain")]
        public string Email { get; set; }

		public string? City { get; set; }

		public List<string> Roles { get; set; } = new();

		public List<string> Claims { get; set; } = new();
	}
}

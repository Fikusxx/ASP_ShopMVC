using Microsoft.AspNetCore.Identity;

namespace Test_ASP_Project.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string? City { get; set; }
	}
}

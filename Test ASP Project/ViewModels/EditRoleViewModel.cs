using System.ComponentModel.DataAnnotations;
using Test_ASP_Project.Models;

namespace Test_ASP_Project.ViewModels
{
	public class EditRoleViewModel
	{
		[Required]
		public string Id { get; set; }

		[Required(ErrorMessage = "Role Name is required")]
		public string RoleName { get; set; }

		public List<string> applicationUsers { get; set; } = new();
	}
}

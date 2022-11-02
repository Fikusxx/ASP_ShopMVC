using System.ComponentModel.DataAnnotations;

namespace Test_ASP_Project.ViewModels
{
	public class CreateRoleViewModel
	{
		[Required]
		public string RoleName { get; set; }
	}
}

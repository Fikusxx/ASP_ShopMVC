using System.Security.Claims;

namespace Test_ASP_Project.ViewModels
{
	public class UserClaimsViewModel
	{
		public string UserId { get; set; }
		public List<UserClaim> Claims { get; set; } = new();
	}
}

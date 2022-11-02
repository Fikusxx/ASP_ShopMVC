using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace Test_ASP_Project.Models
{
	public static class ClaimsStore
	{
		public static List<Claim> ClaimsList = new()
		{
			new Claim("Create Role", "Create Role"),
			new Claim("Edit Role", "Edit Role"),
			new Claim("Delete Role", "Delete Role")
		};
	}
}

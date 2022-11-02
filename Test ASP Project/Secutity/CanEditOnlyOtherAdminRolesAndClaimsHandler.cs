using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Test_ASP_Project.Secutity
{
	public class CanEditOnlyOtherAdminRolesAndClaimsHandler : AuthorizationHandler<ManageAdminRolesAndClaimsRequirement>
	{

		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManageAdminRolesAndClaimsRequirement requirement)
		{
			var authFilterContext = context.Resource as AuthorizationFilterContext;

			if (authFilterContext == null)
				return Task.CompletedTask;

			string loggedInAdminId = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

			string adminIdBeingEdited = authFilterContext.HttpContext.Request.Query["userId"];

			if (context.User.IsInRole("Admin") &&
				context.User.HasClaim(x => x.Type == "Edit Role" && x.Value == "true") &&
				adminIdBeingEdited.ToLower() != loggedInAdminId.ToLower())
			{
				context.Succeed(requirement);
			}

			return Task.CompletedTask;
		}
	}
}

using System.ComponentModel.DataAnnotations;

namespace Test_ASP_Project.Utilities
{
	public class ValidEmailDomainAttribute : ValidationAttribute
	{
		private readonly string allowedDomain;
		public ValidEmailDomainAttribute(string allowedDomain)
		{
            this.allowedDomain = allowedDomain;
		}
		public override bool IsValid(object? value)
		{
			var array = value!.ToString()!.Split('@');

			return array[1].ToUpper() == allowedDomain.ToUpper();
		}
	}
}

using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Test_ASP_Project.Secutity.CustomTokenProviders
{
	public class CustomEmailConfirmationTokenProvider<TUser> 
		: DataProtectorTokenProvider<TUser> where TUser : class
 	{
		public CustomEmailConfirmationTokenProvider(IDataProtectionProvider dataProtectionProvider,
			IOptions<CustomEmailConfirmationTokenProviderOptions> options, 
			ILogger<CustomEmailConfirmationTokenProvider<TUser>> logger) 
			: base(dataProtectionProvider, options, logger)
		{

		}
	}
}

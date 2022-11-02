using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NLog.Extensions.Logging;
using Test_ASP_Project.Models;
using Test_ASP_Project.Secutity;
using Test_ASP_Project.Secutity.CustomTokenProviders;
using Test_ASP_Project.Secutity.Encryption;


var builder = WebApplication.CreateBuilder();
var services = builder.Services;
var configuration = builder.Configuration;

#region Configuration

configuration.AddJsonFile("config.json");

builder.Logging.AddNLog();

// Устанавливаем соединение для EF Core
string connection = configuration.GetConnectionString("EmployeeDBConnection");
services.AddDbContextPool<ApplicationContext>(options => options.UseSqlServer(connection));

services.AddIdentity<ApplicationUser, IdentityRole>()
             .AddEntityFrameworkStores<ApplicationContext>()
             .AddDefaultTokenProviders()
             .AddTokenProvider<CustomEmailConfirmationTokenProvider<ApplicationUser>>("CustomEmailConfirmation");

services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireDigit = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireUppercase = false;

    options.User.RequireUniqueEmail = true;

    options.SignIn.RequireConfirmedEmail = true;

    options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
});

services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(5);
});

services.Configure<CustomEmailConfirmationTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromDays(3);
});

#endregion

#region MVC & Authentication & Authorization

services.AddMvc(options =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});

services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
});

// Добавляем Policy
services.AddAuthorization(options =>
{
    options.AddPolicy("DeleteRolePolicy", policy => policy.RequireClaim("Delete Role", "true"));
    options.AddPolicy("EditRolePolicy", policy => policy.RequireAssertion(context =>
    {
        if ((context.User.IsInRole("Admin") &&
        context.User.HasClaim(x => x.Type == "Edit Role" && x.Value == "true")) ||
        context.User.IsInRole("SuperAdmin"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }));
    options.AddPolicy("CreateRolePolicy", policy => policy.RequireClaim("Create Role"));
    options.AddPolicy("AdminRolePolicy", policy => policy.RequireRole("Admin"));
});

#endregion

#region DI

services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
services.AddSingleton<IAuthorizationHandler, CanEditOnlyOtherAdminRolesAndClaimsHandler>();
services.AddSingleton<DataProtectionPurposeStrings>();

#endregion

#region App Settings

var app = builder.Build();

app.UseStatusCodePagesWithReExecute("/error/{0}");
app.UseExceptionHandler("/error");

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

#endregion

app.Run();





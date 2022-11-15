using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShopMVC_DataAccess;
using ShopMVC_Models;
using ShopMVC_Utility;

var builder = WebApplication.CreateBuilder();
var services = builder.Services;
var configuration = builder.Configuration;

#region Session
services.AddDistributedMemoryCache();
services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromMinutes(10);
    opt.Cookie.HttpOnly = true;
    opt.Cookie.IsEssential = true;
});
#endregion

services.AddControllersWithViews();
services.AddAuthentication().AddFacebook(options =>
{
    options.AppId = "529777545356373";
    options.AppSecret = "ddb125811e405979624429d2f021859b";
});

#region EF Connection

string connection = configuration.GetConnectionString("DefaultConnection");
services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(connection));

#endregion

#region Identity

services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();

#endregion

#region DI Containers

services.AddScoped<ICategoryRepository, CategoryRepository>();
services.AddScoped<IApplicationTypeRepository, ApplicationTypeRepository>();
services.AddScoped<IProductRepository, ProductRepository>();
services.AddScoped<IInquiryHeaderRepository, InquiryHeaderRepository>();
services.AddScoped<IInquiryDetailsRepository, InquiryDetailsRepository>();
services.AddScoped<IOrderHeaderRepository, OrderHeaderRepository>();
services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();
services.AddScoped<IDbInitializer, DbInitializer>();

services.Configure<BrainTreeSettings>(configuration.GetSection("BrainTree"));
services.AddSingleton<IBrainTreeGate, BrainTreeGate>();

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() == false)
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); 

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Map("/", (IDbInitializer db) =>
{
    db.Init();
});

app.Run();

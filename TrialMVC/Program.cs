using Microsoft.EntityFrameworkCore;
using TrialMVC.Data;
using TrialMVC.DataAccess;





var builder = WebApplication.CreateBuilder();
var services = builder.Services;
var configuration = builder.Configuration;


services.AddControllersWithViews();
services.AddScoped<IOrderRepository,SQLOrdersRepository>();

#region Session
services.AddDistributedMemoryCache();
services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromMinutes(10);
    opt.Cookie.HttpOnly = true;
    opt.Cookie.IsEssential = true;
});
#endregion

#region EF Connection

string connection = configuration.GetConnectionString("DefaultConnection");
services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(connection));

#endregion

var app = builder.Build();

#region Error Handling

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

#endregion

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Order}/{action=Index}/{id?}");

app.Run();

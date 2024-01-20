#nullable disable
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;
using PayrollManagementSystem.Data;
using Microsoft.AspNetCore.Identity;
using MyApplication.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using ActionFilterTrace.Models;



var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection");builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddDefaultIdentity<IdentityUser>().AddDefaultTokenProviders().AddRoles<IdentityRole>()
  .AddEntityFrameworkStores<ApplicationDbContext>();

// builder.Services.AddControllersWithViews(options=>{
//     options.Filters.Add(typeof(TraceActivity));
// });
// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddMvc(options=>options.EnableEndpointRouting=false);
builder.Services.AddControllers(options =>
{
    options.ModelMetadataDetailsProviders.Add(new SystemTextJsonValidationMetadataProvider());
});

//configure Ef

builder.Services.AddDbContext<EmployeeDbContext>(option =>{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


//Session

builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(1);
    option.Cookie.HttpOnly =true;
    option.Cookie.IsEssential =true;
});


// builder.Services.AddAuthentication(
//     CookieAuthenticationDefaults.AuthenticationScheme)
//     .AddCookie(option => {
//         option.LoginPath = "/Login/AdminLogin";
//         option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    
//     });

//Cookiee

var app = builder.Build();


// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
if (!app.Environment.IsProduction())
{
    app.UseExceptionHandler("/Error/Error");
    app.UseHsts();
}
//handle 404 and 500 error method=1
app.Use(async(context,next)=>
{
    await next();
    if(context.Response.StatusCode == 404)
    {
        context.Request.Path="/Home/Error";
        await next();
    }
    if(context.Response.StatusCode == 500)
    {
        context.Request.Path="/Home/Error";
        await next();
    }
}
);
//handle 404 and 500 error method=2

// app.UseStatusCodePages(async context=>
// {
    
//     if(context.HttpContext.Response.StatusCode == StatusCodes.Status404NotFound)
//     {
//         context.HttpContext.Request.Path="/Home/Error";
//        await context.HttpContext.Response.WriteAsync("<html><body><h1>Page Not Found</h1></body></html>");
//     }
//     else if(context.HttpContext.Response.StatusCode == StatusCodes.Status500InternalServerError)
//     {
//        context.HttpContext.Request.Path="/Home/Error";
//        await context.HttpContext.Response.WriteAsync("<html><body><h1>Internal Server Error</h1></body></html>");

//     }});
app.UseHttpsRedirection();
app.UseStaticFiles();

//Identity role services pacakge
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapRazorPages();
//Default conventional routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// app.UseEndpoints(endpoints=>
// {
//     endpoints.MapControllers();
// });
// app.UseMvcWithDefaultRoute();
app.Run();


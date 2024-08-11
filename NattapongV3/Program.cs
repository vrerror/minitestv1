using DataAccess;
using DataAccess.Da;
using DataAccess.Entities;
using DataAccess.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NattapongV3.Common;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation()
    .AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.PropertyNamingPolicy = null;
     });

// 2. การเชื่อมฐานข้อมูลใน C#
builder.Services.AddDbContext<CourseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MiniWebMaster")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<CourseContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSession();
builder.Services.AddScoped<FileHelper>();
builder.Services.AddScoped<UserDa>();
builder.Services.AddScoped<ICategoryDa, ProductTypeDa>();
builder.Services.AddScoped<IProductDa, ProductDa>();








var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// การใช้ user password ของ microsoft 25-5-2024 (2)
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
//

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

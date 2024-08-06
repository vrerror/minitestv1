using MinitestTN.Common;
using DataAccess.Da;
using Microsoft.AspNetCore.Identity;
using DataAccess;
using DataAccess.Enities;
using Microsoft.EntityFrameworkCore;
using DataAccess.Interfaces;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });


//builder.Services.AddDbContext<CourseContext>(options =>
//    options.UseMySql(builder.Configuration.GetConnectionString("EFMinitestTN"),
//        new MySqlServerVersion(new Version(8, 0, 39))
//    )
//);


builder.Services.AddDbContext<CourseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MinitestV1")));




builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<CourseContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSession();

builder.Services.AddScoped<IFurnitureCategoryDa,FurnitureCategoryDa>();
builder.Services.AddScoped<FurnitureProductDa>();
builder.Services.AddScoped<FileHelper>();
builder.Services.AddScoped<UserDa>();




// Add services to the container.
builder.Services.AddControllersWithViews();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

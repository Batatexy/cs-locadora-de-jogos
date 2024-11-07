//Tlou ID
//1009


using IGDB;
using IGDB.Models;
using LocadoraDejogos;
using LocadoraDejogos.Data;
using LocadoraDejogos.Models;
using Microsoft.EntityFrameworkCore;
using System.Composition;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(
    builder.Configuration.GetConnectionString("DbPath")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");





app.Run();





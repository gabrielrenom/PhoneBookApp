using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhoneBookApp.DAL.Context;
using PhoneBookApp.DAL.Repository;
using PhoneBookApp.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<PhoneBookDbContext>();
//builder.Services.AddMemoryCache();

builder.Services.AddDbContext<PhoneBookDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("PhoneBookDb")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
builder.Services.AddScoped<IContactsService, ContactsService>();
builder.Services.AddScoped<IContactsRepository, ContactsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();

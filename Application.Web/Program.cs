using Application.Data;
using Application.Data.Models;
using Application.Data.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Identity
builder.Services.AddDbContext<MainContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<User, IdentityRole<int>>(options => { options.SignIn.RequireConfirmedAccount = false; })
    .AddEntityFrameworkStores<MainContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<AuthService>();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    try
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        var users = new List<User>
        {
            new() { UserName = "jan@example.com", Email = "jan@example.com", FirstName = "Jan", LastName = "Jansen", Address = "Straat 1, 1234 AB", PhoneNumber = "0612345678" },
            new() { UserName = "pite@example.com", Email = "pite@example.com", FirstName = "Piet", LastName = "Pietersen", Address = "Straat 2, 1234 CD", PhoneNumber = "0612345678", CustomerCardType = CustomerCardType.Silver },
            new() { UserName = "karin@example.com", Email = "karin@example.com", FirstName = "Karin", LastName = "Klaassen", Address = "Straat 3, 1234 EF", PhoneNumber = "0612345678", CustomerCardType = CustomerCardType.Gold },
            new() { UserName = "sophie@example.com", Email = "sophie@example.com", FirstName = "Sophie", Infix = "de", LastName = "Groot", Address = "Straat 4, 1234 GH", PhoneNumber = "0612345678", CustomerCardType = CustomerCardType.Platinum }
        };

        foreach (var user in users)
        {
            if (await userManager.FindByEmailAsync(user.Email!) == null)
            {
                await userManager.CreateAsync(user, "Password123!");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred during seeding: {ex.Message}");
    }
}

app.Run();
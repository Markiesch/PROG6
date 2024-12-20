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

// Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    try
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        foreach (var role in Enum.GetValues<UserRole>())
        {
            var roleName = role.ToString();
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole<int>(roleName));
            }
        }
        
        var users = new List<User>
        {
            new() { UserName = "customer1", Email = "customer1@example.com", FirstName = "customer", LastName = "1", Address = "Straat 1, 1234 AB", PhoneNumber = "0612345678" },
            new() { UserName = "customer2", Email = "customer2@example.com", FirstName = "Piet", LastName = "2", Address = "Straat 2, 1234 CD", PhoneNumber = "0612345678", CustomerCardType = CustomerCardType.Silver },
            new() { UserName = "admin", Email = "admin@example.com", FirstName = "Admin", LastName = "Admin", Address = "Straat 4, 1234 GH", PhoneNumber = "0612345678", CustomerCardType = CustomerCardType.Platinum }
        };

        foreach (var user in users)
        {
            if (await userManager.FindByEmailAsync(user.Email!) == null)
            {
                await userManager.CreateAsync(user, "Password123!");
                var role = user.UserName == "admin" ? UserRole.Admin : UserRole.Customer;
                await userManager.AddToRoleAsync(user, role.ToString());
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred during seeding: {ex.Message}");
    }
}

app.Run();
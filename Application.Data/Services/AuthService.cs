using Application.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Data.Services;

public class AuthService(UserManager<User> userManager, SignInManager<User> signInManager)
{
    public async Task<User?> Login(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null) return null;

        var result = await signInManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: false);
        return result.Succeeded ? user : null;
    }
    
    public async Task Logout()
    {
        await signInManager.SignOutAsync();
    }
}
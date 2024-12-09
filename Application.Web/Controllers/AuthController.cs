using Application.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Controllers;

public class AuthController(AuthService authService) : Controller
{
    [HttpGet("/login")]
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await authService.Login(email, password);
        if (user == null) return View();
        
        return RedirectToAction("Index", "Home");
    }
}
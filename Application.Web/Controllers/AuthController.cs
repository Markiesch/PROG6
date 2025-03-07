﻿using Application.Data.Models;
using Application.Data.Services;
using Application.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Controllers;

public class AuthController(AuthService authService) : Controller
{
    [HttpGet("/login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("/login")]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        if (!ModelState.IsValid) return View("Login", model);

        var user = await authService.Login(model.Email, model.Password);
        if (user == null)
        {
            const string error = "Email of wachtwoord is onjuist";
            ModelState.AddModelError("Email", error);
            ModelState.AddModelError("Password", error);
            return View("Login", model);
        }

        if (returnUrl != null)
            return Redirect(returnUrl);
        if (User.IsInRole(UserRole.Admin.ToString()))
            return RedirectToAction("Index", "Animal");
        if (User.IsInRole(UserRole.Customer.ToString()))
            return RedirectToAction("Bookings", "Account");

        return RedirectToAction("Index", "Home");
    }

    [HttpPost("/logout")]
    public async Task<IActionResult> Logout()
    {
        await authService.Logout();
        return RedirectToAction("Login");
    }
}
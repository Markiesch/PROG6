using System.Security.Claims;
using Application.Data.Services;
using Application.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Controllers;

[Authorize(Roles = "Customer")]
[Route("/account")]
public class AccountController(AccountService accountService) : Controller
{
    [HttpGet("details")]
    public async Task<IActionResult> Details()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var account = await accountService.GetAccount(userId);
        
        if (account == null)
        {
            return NotFound();
        }

        var model = new AccountDetailsViewModel
        {
            Account = account
        };
        
        return View(model);
    }
    
    [HttpGet("bookings")]
    public async Task<IActionResult> Bookings()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // var bookings = await accountService.GetBookings(userId);
        
        // var model = new AccountBookingsViewModel
        // {
            // Bookings = bookings
        // };
        
        return View();
    }
}
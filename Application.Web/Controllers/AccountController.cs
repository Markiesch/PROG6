using System.Security.Claims;
using Application.Data.Services;
using Application.Web.Models;
using Application.Web.Rules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Controllers;

[Route("/account")]
public class AccountController(AccountService accountService, BookingService bookingService) : Controller
{
    [HttpGet("details")]
    [Authorize(Roles = "Customer, Admin")]
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
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> Bookings(string? query)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var bookings = await bookingService.GetBookings(userId, query);
        
        var model = new BookingListViewModel
        {
            Bookings = bookings
        };
        
        return View(model);
    }
    
    [HttpGet("bookings/{id:int}")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> Booking(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var booking = await bookingService.GetBooking(userId, id);
        var account = await accountService.GetAccount(userId);
        
        if (booking == null || account == null)
        {
            return NotFound();
        }
        
        var subTotal = DiscountRules.CalculateSubTotalPrice(booking.Animals.ToList());
        var discounts = DiscountRules.GetDiscounts(booking.Animals.ToList(), DateOnly.FromDateTime(booking.Date), account.CustomerCardType);

        var model = new BookingDetailsViewModel
        {
            Booking = booking,
            Account = account,
            SubTotal = subTotal,
            Discounts = discounts
        };
        
        return View(model);
    }
    
    [HttpPost("bookings/{id:int}/delete")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> DeleteBooking(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var res = await bookingService.DeleteBooking(userId, id);
        if (!res) return NotFound();
        
        return RedirectToAction("Bookings");
    }
}
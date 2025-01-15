﻿using System.Security.Claims;
using Application.Data.Services;
using Application.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Web.Controllers;

[Authorize(Roles = "Customer")]
[Route("/account")]
public class AccountController(AccountService accountService, BookingService bookingService) : Controller
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
    public async Task<IActionResult> Booking(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var booking = await bookingService.GetBooking(userId, id);
        var account = await accountService.GetAccount(userId);
        
        if (booking == null || account == null)
        {
            return NotFound();
        }

        var model = new BookingDetailsViewModel
        {
            Booking = booking,
            Account = account
        };
        
        return View(model);
    }
    
    [HttpPost("bookings/{id:int}/delete")]
    public async Task<IActionResult> DeleteBooking(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var res = await bookingService.DeleteBooking(userId, id);
        if (!res) return NotFound();
        
        return RedirectToAction("Bookings");
    }
}
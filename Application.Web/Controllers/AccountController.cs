using System.Security.Claims;
using Application.Data.Dto;
using Application.Data.Services;
using Application.Web.Models;
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

        var model = new BookingDetailsViewModel
        {
            Booking = booking,
            Account = account
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

    [HttpGet("create")]
    public IActionResult Create()
    {
        var model = new AccountCreateViewModel();

        return View(model);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(AccountCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var dto = new CreateAccountDto
        {
            FirstName = model.FirstName,
            Infix = model.Infix,
            LastName = model.LastName,
            Email = model.Email,
            Address = model.Address,
            PhoneNumber = model.PhoneNumber,
            CustomerCardType = model.CustomerCardType
        };

        var password = await accountService.CreateAccount(dto);

        if (password == null)
        {
            ModelState.AddModelError("", "Failed to create account");
            return View(model);
        }

        return CreateSuccess(model.Email, password);
    }
    
    [HttpGet("create/success")]
    public IActionResult CreateSuccess(string email, string password)
    {
        var model = new AccountCreateSuccessViewModel
        {
            Email = email,
            Password = password,
        };

        return View(model);
    }
}
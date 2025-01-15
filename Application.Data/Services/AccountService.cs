﻿using Application.Data.Dto;
using Microsoft.EntityFrameworkCore;

namespace Application.Data.Services;

public class AccountService(MainContext context)
{
    public async Task<AccountDto?> GetAccount(int id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null)
        {
            return null;
        }

        return new AccountDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            Infix = user.Infix,
            LastName = user.LastName,
            Email = user.Email,
            Address = user.Address,
            PhoneNumber = user.PhoneNumber,
            CustomerCardType = user.CustomerCardType
        };
    }
    
    public async Task<IEnumerable<BookingDto>> GetBookings(int userId)
    {
        var bookings = await context.Bookings
            .Where(b => b.CustomerId == userId)
            .Select(b => new BookingDto
            {
                Id = b.Id,
                Date = b.Date,
                Animals = b.BookingDetails.Select(a => new AnimalDto
                {
                    Id = a.Animal.Id,
                    Name = a.Animal.Name,
                    Type = a.Animal.Type,
                    Price = a.Animal.Price,
                    Image = a.Animal.Image,
                    IsAvailable = true
                }).ToList()
            })
            .ToListAsync();
        
        return bookings;
    }
}
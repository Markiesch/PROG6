using Application.Data.Dto;
using Microsoft.EntityFrameworkCore;

namespace Application.Data.Services;

public class BookingService(MainContext mainContext)
{
    public async Task<IEnumerable<BookingDto>> GetBookingsByAnimalId(int id, string? query)
    {
        return await mainContext.Bookings
            .Where(b => b.BookingDetails.Any(bd => bd.AnimalId == id))
            .Where(b => string.IsNullOrEmpty(query) || b.Id.ToString() == query)
            .Select(b => new BookingDto
            {
                Id = b.Id,
                Date = b.Date,
                TotalPrice = b.TotalPrice,
                Animals = b.BookingDetails.Select(bd => new AnimalDto
                {
                    Id = bd.Animal.Id,
                    Name = bd.Animal.Name,
                    Type = bd.Animal.Type,
                    Price = bd.Animal.Price,
                    Image = bd.Animal.Image,
                    IsAvailable = true
                })
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<BookingDto>> GetBookings(int userId, string? query)
    {
        var bookings = await mainContext.Bookings
            .Where(b => b.CustomerId == userId)
            .Where(x => string.IsNullOrEmpty(query) || x.Id.ToString().Contains(query))
            .Select(b => new BookingDto
            {
                Id = b.Id,
                Date = b.Date,
                TotalPrice = b.TotalPrice,
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

    public async Task<BookingDto?> GetBooking(int userId, int bookingId)
    {
        var booking = await mainContext.Bookings
            .Where(b => b.CustomerId == userId && b.Id == bookingId)
            .Select(b => new BookingDto
            {
                Id = b.Id,
                Date = b.Date,
                TotalPrice = b.TotalPrice,
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
            .FirstOrDefaultAsync();

        return booking;
    }

    public async Task<bool> DeleteBooking(int userId, int bookingId)
    {
        var booking = await mainContext.Bookings
            .Where(b => b.CustomerId == userId && b.Id == bookingId)
            .FirstOrDefaultAsync();

        if (booking == null)
        {
            return false;
        }

        mainContext.Bookings.Remove(booking);
        await mainContext.SaveChangesAsync();

        return true;
    }
}
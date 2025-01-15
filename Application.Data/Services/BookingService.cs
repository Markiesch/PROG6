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
                Totalprice = b.Totalprice,
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
}
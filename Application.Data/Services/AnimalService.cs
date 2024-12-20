using Application.Data.Dto;
using Application.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Data.Services;

public class AnimalService(MainContext mainContext)
{
    public async Task<List<AnimalDto>> GetAnimals()
    {
        return await mainContext.Animals
            .Select(a => new AnimalDto
            {
                Id = a.Id,
                Name = a.Name,
                Type = a.Type,
                Price = a.Price,
                Image = a.Image,
                IsAvailable = true
            })
            .ToListAsync();
    } 
    
    public async Task<List<AnimalDto>> GetAnimalsWithAvailability(DateOnly date)
    {
        return await mainContext.Animals
            .Select(a => new AnimalDto
            {
                Id = a.Id,
                Name = a.Name,
                Type = a.Type,
                Price = a.Price,
                Image = a.Image,
                IsAvailable = a.BookingDetails.Any(bd => DateOnly.FromDateTime(bd.Booking.Date) == date)
            })
            .ToListAsync();
    } 
}
using Application.Data.Dto;
using Application.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Data.Services;

public class AnimalService(MainContext mainContext, BookingService bookingService)
{
    public async Task<List<AnimalDto>> GetAnimals(string? query)
    {
        return await mainContext.Animals
            .Where(x => string.IsNullOrEmpty(query) || x.Name.Contains(query))
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
    
    public async Task<IEnumerable<BookingDto>?> GetBookings(int id, string? query)
    {
        var animal = await GetAnimal(id);
        if (animal == null) return null;
        
        return await bookingService.GetBookingsByAnimalId(id, query);
    }
    
    public async Task<AnimalDto?> GetAnimal(int id)
    {
        return await mainContext.Animals
            .Where(a => a.Id == id)
            .Select(a => new AnimalDto
            {
                Id = a.Id,
                Name = a.Name,
                Type = a.Type,
                Price = a.Price,
                Image = a.Image,
                IsAvailable = true
            })
            .FirstOrDefaultAsync();
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
    
    
    public async Task<bool> UpdateAnimal(int id, UpdateAnimalDto animal)
    {
        var entity = await mainContext.Animals.FindAsync(id);
        if (entity == null) return false;
        
        entity.Name = animal.Name;
        entity.Type = animal.Type;
        entity.Price = animal.Price;
        entity.Image = animal.Image;
        
        await mainContext.SaveChangesAsync();
        return true;
    }

    public async Task CreateAnimal(UpdateAnimalDto animal)
    {
        var entity = new Animal
        {
            Name = animal.Name,
            Type = animal.Type,
            Price = animal.Price,
            Image = animal.Image
        };
        
        mainContext.Animals.Add(entity);
        await mainContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteAnimal(int id)
    {
        var entity = await mainContext.Animals.FindAsync(id);
        if (entity == null) return false;
        
        mainContext.Animals.Remove(entity);
        await mainContext.SaveChangesAsync();
        return true;
    }
}
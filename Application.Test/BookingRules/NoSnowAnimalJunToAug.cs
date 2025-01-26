using Application.Data.Dto;
using Application.Data.Models;

namespace Application.Test.BookingRules;

public class NoSnowAnimalJunToAug
{
    [Test]
    public void False_When_SnowAnimalBookedInSummer()
    {
        // Arrange
        var animalToAdd = new AnimalDto
        {
            Id = 1,
            Name = "zeehond",
            Type = AnimalType.Snow,
            Price = 100,
            Image = "zeehond.jpg",
            IsAvailable = true
        };
        var date = new DateOnly(2025, 7, 1); // 1st of july
        
        // Act
        var result = Web.Rules.BookingRules.NoSnowAnimalJunToAug(animalToAdd, date, out _);
        
        // Assert
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void True_When_SnowAnimalBookedInWinter()
    {
        // Arrange
        var animalToAdd = new AnimalDto
        {
            Id = 1,
            Name = "zeehond",
            Type = AnimalType.Snow,
            Price = 100,
            Image = "zeehond.jpg",
            IsAvailable = true
        };
        var date = new DateOnly(2025, 12, 1); // 1st of december
        
        // Act
        var result = Web.Rules.BookingRules.NoSnowAnimalJunToAug(animalToAdd, date, out _);
        
        // Assert
        Assert.That(result, Is.True);
    }
}
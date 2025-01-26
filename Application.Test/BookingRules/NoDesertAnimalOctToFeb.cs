using Application.Data.Dto;
using Application.Data.Models;

namespace Application.Test.BookingRules;

public class NoDesertAnimalOctToFeb
{
    [Test]
    public void False_When_DesertAnimalBookedInWinter()
    {
        // Arrange
        var animalToAdd = new AnimalDto
        {
            Id = 1,
            Name = "kameel",
            Type = AnimalType.Desert,
            Price = 100,
            Image = "kameel.jpg",
            IsAvailable = true
        };
        var date = new DateOnly(2025, 12, 1); // 1st of december
        
        // Act
        var result = Web.Rules.BookingRules.NoDesertAnimalOctToFeb(animalToAdd, date, out _);
        
        // Assert
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void True_When_DesertAnimalBookedInSummer()
    {
        // Arrange
        var animalToAdd = new AnimalDto
        {
            Id = 1,
            Name = "kameel",
            Type = AnimalType.Desert,
            Price = 100,
            Image = "kameel.jpg",
            IsAvailable = true
        };
        var date = new DateOnly(2025, 6, 1); // 1st of june
        
        // Act
        var result = Web.Rules.BookingRules.NoDesertAnimalOctToFeb(animalToAdd, date, out _);
        
        // Assert
        Assert.That(result, Is.True);
    }
}
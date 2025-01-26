using Application.Data.Dto;
using Application.Data.Models;

namespace Application.Test.BookingRules;

public class NoPenguinOnWeekend
{
    [Test]
    public void False_When_PenguinBookedOnWeekend()
    {
        // Arrange
        var animalToAdd = new AnimalDto
        {
            Id = 1,
            Name = "pinguin",
            Type = AnimalType.Snow,
            Price = 100,
            Image = "pinguin.jpg",
            IsAvailable = true
        };
        var date = new DateOnly(2025, 1, 25); // saturday
        
        // Act
        var result = Web.Rules.BookingRules.NoPenguinOnWeekend(animalToAdd, date, out _);
        
        // Assert
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void True_When_PenguinBookedOnWeekdays()
    {
        // Arrange
        var animalToAdd = new AnimalDto
        {
            Id = 1,
            Name = "pinguin",
            Type = AnimalType.Snow,
            Price = 100,
            Image = "pinguin.jpg",
            IsAvailable = true
        };
        var date = new DateOnly(2025, 1, 27); // monday
        
        // Act
        var result = Web.Rules.BookingRules.NoPenguinOnWeekend(animalToAdd, date, out _);
        
        // Assert
        Assert.That(result, Is.True);
    }
}
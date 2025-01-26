using Application.Data.Dto;
using Application.Data.Models;

namespace Application.Test.BookingRules;

public class ValidateCustomerCard
{
    [Test]
    public void False_When_OrderNotAllowedWithCustomerCard()
    {
        // Arrange
        var animalToAdd = new AnimalDto
        {
            Id = 1,
            Name = "Eenhoorn",
            Type = AnimalType.Vip,
            Price = 100,
            Image = "Eenhoorn.jpg",
            IsAvailable = true
        };
        var selectedAnimals = new List<AnimalDto>
        {
            new()
            {
                Id = 2,
                Name = "olifant",
                Type = AnimalType.Jungle,
                Price = 100,
                Image = "olifant.jpg",
                IsAvailable = true
            }
        };

        // Act
        var result = Web.Rules.BookingRules.ValidateCustomerCard(animalToAdd, selectedAnimals, CustomerCardType.Silver, out _);
        
        // Assert
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void True_When_OrderAllowedWithCustomerCard()
    {
        // Arrange
        var animalToAdd = new AnimalDto
        {
            Id = 1,
            Name = "Eenhoorn",
            Type = AnimalType.Vip,
            Price = 100,
            Image = "Eenhoorn.jpg",
            IsAvailable = true
        };
        var selectedAnimals = new List<AnimalDto>
        {
            new()
            {
                Id = 2,
                Name = "olifant",
                Type = AnimalType.Jungle,
                Price = 100,
                Image = "olifant.jpg",
                IsAvailable = true
            }
        };

        // Act
        var result = Web.Rules.BookingRules.ValidateCustomerCard(animalToAdd, selectedAnimals, CustomerCardType.Platinum, out _);
        
        // Assert
        Assert.That(result, Is.True);
    }
}
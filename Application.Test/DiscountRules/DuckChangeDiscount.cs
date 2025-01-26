using Application.Data.Dto;
using Application.Data.Models;

namespace Application.Test.DiscountRules;

public class DuckChangeDiscount
{
    [Test]
    public void Zero_WhenNoDuckInBooking()
    {
        // Arrange
        var animals = new List<AnimalDto>
        {
            new()
            {
                Id = 1,
                Name = "ezel",
                Type = AnimalType.Farm,
                Price = 100,
                Image = "ezel.jpg",
                IsAvailable = true
            },
            new()
            {
                Id = 2,
                Name = "koe",
                Type = AnimalType.Farm,
                Price = 100,
                Image = "koe.jpg",
                IsAvailable = true
            }
        };

        // Act
        var result = Web.Rules.DiscountRules.DuckChangeDiscount(animals);

        // Assert
        Assert.That(result, Is.Zero);
    }
    
    [Test]
    public void NotZero_WhenDuckInBooking_AndRandomReturnsOne()
    {
        // Arrange
        var animals = new List<AnimalDto>
        {
            new()
            {
                Id = 1,
                Name = "eend",
                Type = AnimalType.Farm,
                Price = 100,
                Image = "eend.jpg",
                IsAvailable = true
            }
        };

        // Act
        int result;
        while (true) // Loop until random returns 1
        {
            result = Web.Rules.DiscountRules.DuckChangeDiscount(animals);
            if (result != 0) break;
        }

        // Assert
        Assert.That(result, Is.Not.Zero);
    }
}
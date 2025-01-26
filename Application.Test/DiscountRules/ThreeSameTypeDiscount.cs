using Application.Data.Dto;
using Application.Data.Models;

namespace Application.Test.DiscountRules;

public class ThreeSameTypeDiscount
{
    [Test]
    public void Zero_When_NotThreeSameTypes()
    {
        // Arrange
        var animals = new List<AnimalDto>
        {
            new()
            {
                Id = 1,
                Name = "kameel",
                Type = AnimalType.Desert,
                Price = 100,
                Image = "kameel.jpg",
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
        var result = Web.Rules.DiscountRules.ThreeSameTypeDiscount(animals);

        // Assert
        Assert.That(result, Is.Zero);
    }
    
    [Test]
    public void NotZero_When_ThreeSameTypes()
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
            },
            new()
            {
                Id = 3,
                Name = "eend",
                Type = AnimalType.Farm,
                Price = 100,
                Image = "eend.jpg",
                IsAvailable = true
            }
        };

        // Act
        var result = Web.Rules.DiscountRules.ThreeSameTypeDiscount(animals);

        // Assert
        Assert.That(result, Is.Not.Zero);
    }
}
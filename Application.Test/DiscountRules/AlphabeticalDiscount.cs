using Application.Data.Dto;
using Application.Data.Models;

namespace Application.Test.DiscountRules;

public class AlphabeticalDiscount
{
    [Test]
    public void Zero_WhenNoAlphabeticalDiscount()
    {
        // Arrange
        var animals = new List<AnimalDto>
        {
            new()
            {
                Id = 1,
                Name = "eend",
                Type = AnimalType.Desert,
                Price = 100,
                Image = "eend.jpg",
                IsAvailable = true
            },
            new()
            {
                Id = 2,
                Name = "leeuw",
                Type = AnimalType.Jungle,
                Price = 100,
                Image = "leeuw.jpg",
                IsAvailable = true
            }
        };

        // Act
        var result = Web.Rules.DiscountRules.AlphabeticalDiscount(animals);

        // Assert
        Assert.That(result, Is.Zero);
    }
    
    [Test]
    public void NotZero_WhenAlphabeticalDiscount()
    {
        // Arrange
        var animals = new List<AnimalDto>
        {
            new()
            {
                Id = 1,
                Name = "aap",
                Type = AnimalType.Desert,
                Price = 100,
                Image = "aap.jpg",
                IsAvailable = true
            },
            new()
            {
                Id = 2,
                Name = "zebra",
                Type = AnimalType.Jungle,
                Price = 100,
                Image = "zebra.jpg",
                IsAvailable = true
            }
        };

        // Act
        var result = Web.Rules.DiscountRules.AlphabeticalDiscount(animals);

        // Assert
        Assert.That(result, Is.Not.Zero);
    }
}
using Application.Data.Dto;
using Application.Data.Models;

namespace Application.Test.DiscountRules;

public class CalculateSubTotalPrice
{
    [Test]
    public void True_When_SubtotalIsCorrect()
    {
        // Arrange
        var animals = new List<AnimalDto>
        {
            new()
            {
                Id = 1,
                Name = "leeuw",
                Type = AnimalType.Jungle,
                Price = 45,
                Image = "leeuw.jpg",
                IsAvailable = true
            },
            new()
            {
                Id = 2,
                Name = "olifant",
                Type = AnimalType.Jungle,
                Price = 25,
                Image = "olifant.jpg",
                IsAvailable = true
            }
        };
        
        // Act
        var result = Web.Rules.DiscountRules.CalculateSubTotalPrice(animals);
        
        // Assert
        Assert.That(result, Is.EqualTo(70));
    }
}
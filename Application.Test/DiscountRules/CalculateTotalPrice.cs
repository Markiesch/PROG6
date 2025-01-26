using Application.Data.Dto;
using Application.Data.Models;

namespace Application.Test.DiscountRules;

public class CalculateTotalPrice
{
    [Test]
    public void True_When_TotalPriceIsCorrect()
    {
        // Arrange
        const int subtotal = 85;
        var discount = new Dictionary<string, int>
        {
            { "3 Zelfde types", 10 },
            { "Maandag of dinsdag", 15 }
        };
        
        // Act
        var result = Web.Rules.DiscountRules.CalculateTotalPrice(subtotal, discount);
        
        // Assert
        Assert.That(result, Is.EqualTo(63.75));
    }
}
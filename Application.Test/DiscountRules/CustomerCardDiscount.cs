using Application.Data.Models;

namespace Application.Test.DiscountRules;

public class CustomerCardDiscount
{
    [Test]
    public void Zero_WhenNoCustomerCard()
    {
        // Arrange
        CustomerCardType? customerCard = null;
        
        // Act
        var result = Web.Rules.DiscountRules.CustomerCardDiscount(customerCard);

        // Assert
        Assert.That(result, Is.Zero);
    }
    
    [Test]
    public void NotZero_WhenCustomerCard()
    {
        // Arrange
        CustomerCardType? customerCard = CustomerCardType.Gold;
        
        // Act
        var result = Web.Rules.DiscountRules.CustomerCardDiscount(customerCard);

        // Assert
        Assert.That(result, Is.Not.Zero);
    }
}
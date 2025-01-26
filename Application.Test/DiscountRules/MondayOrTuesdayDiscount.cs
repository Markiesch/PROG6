namespace Application.Test.DiscountRules;

public class MondayOrTuesdayDiscount
{
    [Test]
    public void Zero_WhenNotMondayOrTuesday()
    {
        // Arrange
        var date = new DateOnly(2025, 1, 25); // saturday

        // Act
        var result = Web.Rules.DiscountRules.MondayOrTuesdayDiscount(date);

        // Assert
        Assert.That(result, Is.Zero);
    }
    
    [Test]
    public void NotZero_WhenMondayOrTuesday()
    {
        // Arrange
        var date = new DateOnly(2025, 1, 27); // monday

        // Act
        var result = Web.Rules.DiscountRules.MondayOrTuesdayDiscount(date);

        // Assert
        Assert.That(result, Is.Not.Zero);
    }
}
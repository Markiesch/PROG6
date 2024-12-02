namespace Application.Data.Models;

public class Animal
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public AnimalType Type { get; init; }
    public decimal Price { get; init; }
    public string? Image { get; init; }

    public ICollection<BookingDetail> BookingDetails { get; init; } = new List<BookingDetail>();
}
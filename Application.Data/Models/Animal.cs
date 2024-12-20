namespace Application.Data.Models;

public class Animal
{
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public AnimalType Type { get; set; }
    public decimal Price { get; set; }
    public string? Image { get; set; }

    public ICollection<BookingDetail> BookingDetails { get; init; } = new List<BookingDetail>();
}
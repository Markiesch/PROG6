namespace Application.Data.Models;

public class BookingDetail
{
    public int Id { get; init; }
    public int BookingId { get; init; }
    public int AnimalId { get; init; }

    public Booking Booking { get; init; } = null!;
    public Animal Animal { get; init; } = null!;
}
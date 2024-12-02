namespace Application.Data.Models;

public class User
{
    public int Id { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string? Infix { get; init; }
    public string LastName { get; init; } = string.Empty;
    public string Address { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public CustomerCardType? CustomerCardType { get; init; }

    public ICollection<Booking> Bookings { get; init; } = new List<Booking>();
}
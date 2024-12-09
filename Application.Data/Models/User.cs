using Microsoft.AspNetCore.Identity;

namespace Application.Data.Models;

public class User : IdentityUser<string>
{
    public string FirstName { get; init; } = string.Empty;
    public string? Infix { get; init; }
    public string LastName { get; init; } = string.Empty;
    public string Address { get; init; } = string.Empty;
    public CustomerCardType? CustomerCardType { get; init; }

    public ICollection<Booking> Bookings { get; init; } = new List<Booking>();
}
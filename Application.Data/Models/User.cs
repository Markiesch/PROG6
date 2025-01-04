using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Application.Data.Models;

public class User : IdentityUser<int>
{
    [Required] 
    [MaxLength(255)] 
    public string FirstName { get; init; } = string.Empty;
    
    [MaxLength(5)]
    public string? Infix { get; init; }
    
    [Required]
    [MaxLength(255)]
    public string LastName { get; init; }
    
    [Required]
    [MaxLength(255)]
    public string Address { get; init; }
    
    [EnumDataType(typeof(CustomerCardType))]
    public CustomerCardType? CustomerCardType { get; init; }

    [InverseProperty("Customer")]
    public ICollection<Booking> Bookings { get; init; } = new List<Booking>();
}
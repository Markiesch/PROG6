using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models;

public class Booking
{
    [Key]
    public int Id { get; init; }
    
    [Required]
    public DateTime Date { get; init; }
    
    [Required]
    public bool Confirmed { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Totalprice { get; init; }

    [Required]
    public int CustomerId { get; init; }
    
    [ForeignKey("CustomerId")]
    [InverseProperty("Bookings")]
    public User Customer { get; init; } = null!;
    
    [InverseProperty("Booking")]
    public ICollection<BookingDetail> BookingDetails { get; init; } = new List<BookingDetail>();
}
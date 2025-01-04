using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models;

public class BookingDetail
{
    [Key]
    public int Id { get; init; }
    
    [Required]
    public int BookingId { get; init; }

    [Required]
    public int AnimalId { get; init; }

    [ForeignKey("BookingId")]
    [InverseProperty("BookingDetails")]
    public Booking Booking { get; init; } = null!;
    
    [ForeignKey("AnimalId")]
    [InverseProperty("BookingDetails")]
    public Animal Animal { get; init; } = null!;
}
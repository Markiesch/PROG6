using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models;

public class Animal
{
    [Key]
    public int Id { get; init; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    [Required]
    [EnumDataType(typeof(AnimalType))]
    public required AnimalType Type { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public required decimal Price { get; set; }

    [Required]
    [StringLength(200)]
    public required string Image { get; set; }
    
    [InverseProperty("Animal")]
    public ICollection<BookingDetail> BookingDetails { get; init; } = new List<BookingDetail>();
}
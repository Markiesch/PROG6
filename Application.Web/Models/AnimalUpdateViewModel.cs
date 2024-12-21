using System.ComponentModel.DataAnnotations;
using Application.Data.Dto;
using Application.Data.Models;

namespace Application.Web.Models;

public class AnimalUpdateViewModel
{
    public required int Id { get; set; }

    [Required(ErrorMessage = "Naam is verplicht")]
    [MaxLength(100, ErrorMessage = "Naam mag maximaal 100 karakters bevatten")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Type is verplicht")]
    [EnumDataType(typeof(AnimalType), ErrorMessage = "Type is ongeldig")]
    public required AnimalType Type { get; set; }
    
    [Required(ErrorMessage = "Prijs is verplicht")]
    [DataType(DataType.Currency)]
    [Range(0.01, double.MaxValue, ErrorMessage = "Prijs moet minimaal 0.01 zijn")]
    public required decimal Price { get; set; }
    
    [Required(ErrorMessage = "Afbeelding is verplicht")]
    [StringLength(200, ErrorMessage = "Afbeelding mag maximaal 200 karakters bevatten")]
    public required string Image { get; set; }
    
    public UpdateAnimalDto ToDto()
    {
        return new UpdateAnimalDto
        {
            Name = Name,
            Type = Type,
            Price = Price,
            Image = Image,
        };
    }
}
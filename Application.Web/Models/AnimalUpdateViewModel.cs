using Application.Data.Dto;
using Application.Data.Models;

namespace Application.Web.Models;

public class AnimalUpdateViewModel
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required AnimalType Type { get; set; }
    public required decimal Price { get; set; }
    public required string? Image { get; set; }
    
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
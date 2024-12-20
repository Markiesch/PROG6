using Application.Data.Models;

namespace Application.Data.Dto;

public class UpdateAnimalDto
{
    public required string Name { get; init; }
    public required AnimalType Type { get; init; }
    public required decimal Price { get; init; }
    public required string? Image { get; init; }
}
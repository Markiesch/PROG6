using Application.Data.Models;

namespace Application.Data.Dto;

public class AnimalDto
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required AnimalType Type { get; init; }
    public required decimal Price { get; init; }
    public required string? Image { get; init; }
    public required bool IsAvailable { get; init; }
}
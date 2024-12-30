using Application.Data.Models;

namespace Application.Data.Dto;

public class AnimalSelectionRequest
{
    public required int AnimalToAddId { get; init; }
    public required List<int> SelectedAnimalIds { get; init; } 
    public required DateOnly Date { get; init; }
    public required string? CustomerCard { get; init; }
}
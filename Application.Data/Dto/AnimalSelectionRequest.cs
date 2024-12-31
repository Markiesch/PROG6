namespace Application.Data.Dto;

public class AnimalSelectionRequest
{
    public required int AnimalToAddId { get; set; }
    public required List<int> SelectedAnimalIds { get; set; } 
    public required DateOnly Date { get; set; }
    public required string? CustomerCard { get; set; }
}
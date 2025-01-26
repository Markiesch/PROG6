using Application.Data.Dto;
using Application.Data.Models;

namespace Application.Web.Models;

public class PickYourAnimalViewModel
{
    public required DateOnly Date { get; init; }
    public required IEnumerable<AnimalDto> Animals { get; init; }
    public required IEnumerable<AnimalDto> SelectedAnimals { get; init; }
    public CustomerCardType? CustomerCard { get; init; } 
    public decimal? Subtotal { get; init; }
}
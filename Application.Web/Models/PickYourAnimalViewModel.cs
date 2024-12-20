using Application.Data.Dto;
using Application.Data.Models;

namespace Application.Web.Models;

public class PickYourAnimalViewModel
{
    public required DateOnly Date { get; init; }
    
    public required IEnumerable<AnimalDto> Animals { get; init; }
}
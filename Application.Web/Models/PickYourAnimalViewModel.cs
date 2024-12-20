using Application.Data.Dto;
using Application.Data.Models;

namespace Application.Web.Models;

public class PickYourAnimalViewModel
{
    public required DateOnly Date { get; set; }
    
    public required IEnumerable<AnimalDto> Animals { get; set; }
    
    // TODO: remove
    public required List<Animal> UnavailableAnimals { get; set; }
}
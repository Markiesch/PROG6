using Application.Data.Models;

namespace Application.Web.Models;

public class BookingStep2ViewModel
{
    public required DateOnly Date { get; set; }
    
    public required List<Animal> Animals { get; set; }
    
    public required List<Animal> UnavailableAnimals { get; set; }
}
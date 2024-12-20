using Application.Data.Models;

namespace Application.Web.Models;

public class BookingStep3ViewModel
{
    public required DateOnly Date { get; set; }
    
    public required List<Animal> SelectedAnimals { get; set; }
}
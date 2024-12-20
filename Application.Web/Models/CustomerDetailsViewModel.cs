using Application.Data.Models;

namespace Application.Web.Models;

public class CustomerDetailsViewModel
{
    public required DateOnly Date { get; set; }
    
    public required List<Animal> SelectedAnimals { get; set; }
}
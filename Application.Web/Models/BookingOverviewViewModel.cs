using Application.Data.Dto;

namespace Application.Web.Models;

public class BookingOverviewViewModel
{
    public required DateOnly Date { get; init; }
    public required IEnumerable<AnimalDto> SelectedAnimals { get; init; }
    public required CustomerDto Customer { get; init; }
    public required Dictionary<string, int>? Discounts { get; init; }
    public required decimal SubTotalPrice { get; init; }
    public required decimal TotalPrice { get; init; }
}
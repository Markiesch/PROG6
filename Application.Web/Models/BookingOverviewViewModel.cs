using Application.Data.Dto;

namespace Application.Web.Models;

public class BookingOverviewViewModel
{
    public required DateOnly Date { get; init; }
    public required IEnumerable<AnimalDto> SelectedAnimals { get; init; }
}
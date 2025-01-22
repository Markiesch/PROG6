namespace Application.Data.Dto;

public class BookingDto
{
    public required int Id { get; init; }
    public required DateTime Date { get; init; }
    public required decimal TotalPrice { get; init; }
    public required IEnumerable<AnimalDto> Animals { get; init; }
}
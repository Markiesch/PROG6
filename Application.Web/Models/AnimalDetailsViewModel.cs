using Application.Data.Dto;

namespace Application.Web.Models;

public class AnimalDetailsViewModel
{
    public required IEnumerable<BookingDto> Bookings { get; set; }
}
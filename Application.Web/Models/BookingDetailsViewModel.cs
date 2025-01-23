using Application.Data.Dto;

namespace Application.Web.Models;

public class BookingDetailsViewModel
{
    public required BookingDto Booking { get; init; }
    public required AccountDto Account { get; init; }
    public required decimal SubTotal { get; init; }
    public Dictionary<string, int>? Discounts { get; init; }
}
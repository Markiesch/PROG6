using Application.Data.Dto;

namespace Application.Web.Models;

public class BookingListViewModel
{
    public IEnumerable<BookingDto> Bookings { get; set; }
}
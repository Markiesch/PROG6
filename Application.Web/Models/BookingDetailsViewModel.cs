using Application.Data.Dto;

namespace Application.Web.Models;

public class BookingDetailsViewModel
{
    public BookingDto Booking { get; set; }
    public AccountDto Account { get; set; }
}
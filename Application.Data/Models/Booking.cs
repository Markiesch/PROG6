namespace Application.Data.Models;

public class Booking
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public bool Confirmed { get; set; }
    public decimal Totalprice { get; set; }

    // Relaties
    public int CustomerId { get; set; }
    public User Customer { get; set; } = null!;
    public ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();
}
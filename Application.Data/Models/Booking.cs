namespace Application.Data.Models;

public class Booking
{
    public int Id { get; set; }
    public DateTime Datum { get; set; }
    public bool Bevestigd { get; set; }
    public decimal Totaalprijs { get; set; }

    // Relaties
    public int KlantId { get; set; }
    public User Klant { get; set; } = null!;
    public ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();
}
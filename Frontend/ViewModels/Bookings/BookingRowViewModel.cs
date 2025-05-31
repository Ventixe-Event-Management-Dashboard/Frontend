using System.ComponentModel.DataAnnotations.Schema;

namespace Frontend.ViewModels.Bookings;

public class BookingRowViewModel
{
    public Guid BookingId { get; set; }

    //[ForeignKey(nameof(Invoice))]
    public Guid InvpoiceId { get; set; }
    public DateTime Time { get; set; }
    public string Name { get; set; } = null!;
    public string Event { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Total => Price * Quantity;

}
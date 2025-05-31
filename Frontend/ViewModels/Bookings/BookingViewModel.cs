namespace Frontend.ViewModels.Bookings;

public class BookingViewModel
{
    public int TotalBookings { get; set; }
    public int TotalTicketsSold { get; set; }
    public int TotalEarnings { get; set; }

    public List<BookingRowViewModel> Bookings { get; set; } = new List<BookingRowViewModel>();
}

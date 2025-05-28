namespace Frontend.Models.InvoiceModel;

public class TicketItem
{
    public int Id { get; set; }
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Total => Price * Quantity;
}

namespace Frontend.Models.InvoiceModel;

public class Invoice
{
    public string Id { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public bool IsPaid { get; set; }
}

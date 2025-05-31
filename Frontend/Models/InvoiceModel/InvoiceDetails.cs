namespace Frontend.Models.InvoiceModel;

public class InvoiceDetails
{
    public int Id { get; set; }
    public string InvoiceId { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string BookingId { get; set; } = string.Empty;
    public string BillFormName { get; set; } = string.Empty;
    public string BillFormAddress { get; set; } = string.Empty;
    public string BillFormEmail { get; set; } = string.Empty;
    public string BillFormPhone { get; set; } = string.Empty;
    public string BillToName { get; set; } = string.Empty;
    public string BillToAddress { get; set; } = string.Empty;
    public string BillToEmail { get; set; } = string.Empty;
    public string BillToPhone { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public decimal Tax { get; set; }
    public decimal Fee { get; set; }
    public List<TicketItem> Items { get; set; } = new();
}

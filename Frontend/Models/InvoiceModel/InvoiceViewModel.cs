namespace Frontend.Models.InvoiceModel
{
    public class InvoiceViewModel
    {
        public List<Invoice> Invoices { get; set; } = new();
        public InvoiceDetails? SelectedInvoice { get; set; }
    }
}

using Frontend.Models.InvoiceModel;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Frontend.Controllers;

public class InvoicesController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _config;

    public InvoicesController(IHttpClientFactory httpClientFactory, IConfiguration config)
    {
        _httpClientFactory = httpClientFactory;
        _config = config;
    }

    private HttpClient CreateInvoiceClient()
    {
        var client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri(_config["InvoiceService:BaseUrl"]);
        client.DefaultRequestHeaders.Add("X-API-KEY", _config["InvoiceService:ApiKey"]);
        return client;
    }

    public async Task<IActionResult> Index(string id = null)
    {
        ViewData["ActivePage"] = "invoices";
        var client = CreateInvoiceClient();

        var invoices = await client.GetFromJsonAsync<List<Invoice>>("api/invoices") ?? new();

        InvoiceDetails? selected = null;
        if (!string.IsNullOrEmpty(id))
        {
            selected = await client.GetFromJsonAsync<InvoiceDetails>($"api/invoicedetails/{id}");
        }
        else if (invoices.Any())
        {
            selected = await client.GetFromJsonAsync<InvoiceDetails>($"api/invoicedetails/{invoices.First().Id}");
        }

        var model = new InvoiceViewModel
        {
            Invoices = invoices,
            SelectedInvoice = selected
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteInvoice(string id)
    {
        var client = CreateInvoiceClient();
        var response = await client.DeleteAsync($"api/invoices/{id}");
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateInvoice(InvoiceDetails updated)
    {
        var client = CreateInvoiceClient();
        var response = await client.PutAsJsonAsync("api/invoicedetails", updated);
        return RedirectToAction("Index", new { id = updated.InvoiceId });
    }
}
using Frontend.Models.Event;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;

namespace Frontend.Controllers;

public class EventsController : Controller
{
    private readonly HttpClient _httpClient;

    public EventsController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("EventService");
    }

    public async Task<IActionResult> Index(
     string view = "grid",
     int page = 1,
     int pageSize = 8,
     string? searchQuery = null,
     string? selectedCategory = null,
     string? selectedTimeRange = null)
    {
        var queryParams = new Dictionary<string, string?>
        {
            ["search"] = searchQuery,
            ["category"] = selectedCategory,
            ["date"] = selectedTimeRange,
            ["page"] = page.ToString(),
            ["pageSize"] = pageSize.ToString()
        };

        var query = string.Join("&", queryParams
            .Where(kv => !string.IsNullOrWhiteSpace(kv.Value))
            .Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value!)}"));

        var response = await _httpClient.GetFromJsonAsync<EventFilteredResponse>($"/api/v1/Event/filter?{query}");

        if (response is null)
            return View(new EventViewModel());

        var model = new EventViewModel
        {
            Events = response.Events,
            CurrentPage = page,
            PageSize = pageSize,
            TotalEvents = response.TotalCount,
            SearchQuery = searchQuery,
            SelectedCategory = selectedCategory,
            SelectedTimeRange = selectedTimeRange,
            IsGridView = view == "grid"
        };

        return View(model);
    }
}

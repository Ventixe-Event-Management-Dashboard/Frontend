using Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Frontend.Controllers
{
    public class InboxController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : Controller
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IHttpContextAccessor _contextAccessor = httpContextAccessor;

        private void AddJwtFromCookie()
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;

            var token = _contextAccessor.HttpContext?.Request.Cookies["jwt"];
            if (!string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<IActionResult> Index(string folder = "inbox")
        {
            AddJwtFromCookie();

            var endpoint = folder.ToLower() == "sent"
                ? "api/message/sent"
                : "api/message/inbox";

            var response = await _httpClient.GetAsync($"https://ventixeinbox.azurewebsites.net/{endpoint}");

            if (!response.IsSuccessStatusCode)
                return Unauthorized();

            var json = await response.Content.ReadAsStringAsync();
            var messages = JsonSerializer.Deserialize<List<MessageItem>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var viewModel = new InboxViewModel
            {
                Messages = messages ?? [],
                CurrentFolder = folder
            };

            return View("Index", viewModel);
        }

        public async Task<IActionResult> ViewMessage(Guid id, string folder = "inbox")
        {
            AddJwtFromCookie();

            // 1. Hämta lista
            var folderEndpoint = folder.ToLower() == "sent"
                ? "api/message/sent"
                : "api/message/inbox";

            var messagesResponse = await _httpClient.GetAsync($"https://ventixeinbox.azurewebsites.net/{folderEndpoint}");
            var messagesJson = await messagesResponse.Content.ReadAsStringAsync();
            var messages = JsonSerializer.Deserialize<List<MessageItem>>(messagesJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? [];

            // 2. Hämta enskilt meddelande
            var detailResponse = await _httpClient.GetAsync($"https://ventixeinbox.azurewebsites.net/api/message/{id}");
            if (!detailResponse.IsSuccessStatusCode)
                return RedirectToAction("Index", new { folder });

            var detailJson = await detailResponse.Content.ReadAsStringAsync();
            var selected = JsonSerializer.Deserialize<MessageItem>(detailJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var viewModel = new InboxViewModel
            {
                Messages = messages,
                SelectedMessage = selected,
                CurrentFolder = folder
            };

            return View("Index", viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Compose()
        {
            AddJwtFromCookie();

            var response = await _httpClient.GetAsync("https://ventixeinbox.azurewebsites.net/api/message/recipients");

            if (!response.IsSuccessStatusCode)
                return Unauthorized();

            var json = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<UserDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var model = new SendMessageViewModel
            {
                Recipients = users ?? []
            };

            return View("Compose", model);
        }

        [HttpPost]
        public async Task<IActionResult> Compose([FromBody] SendMessageViewModel model)
        {
            AddJwtFromCookie();

            var payload = new
            {
                receiverId = model.ReceiverId,
                subject = model.Subject,
                content = model.Content
            };

            var response = await _httpClient.PostAsJsonAsync("https://ventixeinbox.azurewebsites.net/api/message", payload);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Fel från Azure-backend: " + error);
                return StatusCode((int)response.StatusCode, error);
            }

            return Ok("Meddelande skickat!");
        }
        [HttpGet]
        public async Task<IActionResult> GetComposeForm()
        {
            AddJwtFromCookie();

            var response = await _httpClient.GetAsync("https://ventixeinbox.azurewebsites.net/api/message/recipients");

            if (!response.IsSuccessStatusCode)
                return Unauthorized();

            var json = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<UserDto>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var model = new SendMessageViewModel
            {
                Recipients = users ?? []
            };

            return PartialView("Partials/_ComposeForm", model);
        }
    }
}

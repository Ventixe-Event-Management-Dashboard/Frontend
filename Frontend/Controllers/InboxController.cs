using Frontend.Models;
using Microsoft.AspNetCore.Mvc;
namespace Frontend.Controllers
{
    public class InboxController : Controller
    {
        public IActionResult Index()
        {
            ViewData["ActivePage"] = "inbox";

            var messages = GetMockMessages();

            var viewModel = new InboxViewModel
            {
                Messages = messages,
                SelectedMessage = messages.First()
            };

            return View(viewModel);
        }

        public IActionResult GetMessageDetail(int id)
        {
            var message = GetMockMessages().FirstOrDefault(m => m.Id == id);
            if (message == null) return NotFound();

            return PartialView("Partials/_InboxMessageDetails", message);
        }

        public IActionResult Folder(string folder)
        {
            var messages = GetMockMessages()
                .Where(m => m.Label.Equals(folder, StringComparison.OrdinalIgnoreCase))
                .ToList();

            var viewModel = new InboxViewModel
            {
                Messages = messages,
                SelectedMessage = messages.FirstOrDefault()
            };

            return View("Index", viewModel);
        }

        private List<MessageItem> GetMockMessages()
        {
            return new List<MessageItem>
            {
                new MessageItem
                {
                    Id = 1,
                    Sender = "ANTON",
                    Subject = "TESTING",
                    Preview = "TESTINGTESTINGTESTING",
                    Content = "TESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTINGTESTING",
                    SentAt = DateTime.Now.AddHours(-2),
                    Label = "Sent"
                },
                new MessageItem
                {
                    Id = 2,
                    Sender = "WALTER",
                    Subject = "TESTINGTESTING",
                    Preview = "TESTINGTESTINGTESTING",
                    Content = "TESTINGTESTINGTESTINGTESTING",
                    SentAt = DateTime.Now.AddHours(-5),
                    Label = "Inbox"
                },
                new MessageItem()
                {
                    Id = 3,
                    Sender = "TESTING",
                    Subject = "TESTERRRRR",
                    Preview = "GOTT E DEEEE",
                    Content = "testing testing testing testing testing testing testing testing",
                    SentAt = DateTime.Now.AddHours(-2),
                    Label = "Sent"
                }
            };
        }
    }
}


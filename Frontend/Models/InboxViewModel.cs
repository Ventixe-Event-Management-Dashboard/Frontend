namespace Frontend.Models;

public class InboxViewModel
{
    public List<MessageItem> Messages { get; set; } = [];
    public MessageItem? SelectedMessage { get; set; }
    public string CurrentFolder { get; set; } = "inbox";
}

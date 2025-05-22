namespace Frontend.Models;

public class InboxViewModel
{
    public List<MessageItem> Messages { get; set; } = null!;
    public MessageItem SelectedMessage { get; set; } = null!;
}

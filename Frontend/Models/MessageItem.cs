namespace Frontend.Models
{
    public class MessageItem
    {
        public int Id { get; set; }
        public string Sender { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Preview { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime SentAt { get; set; }
        public string Label { get; set; } = null!;
    }
}

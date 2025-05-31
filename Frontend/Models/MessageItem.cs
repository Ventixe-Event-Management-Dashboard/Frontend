namespace Frontend.Models
{
    public class MessageItem
    {
        public Guid Id { get; set; }
        public string Sender { get; set; } = null!;
        public string Receiver { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime SentAt { get; set; }

        //  Preview-egenskap som visas i listan
        public string Preview =>
            string.IsNullOrWhiteSpace(Content)
                ? ""
                : Content.Length > 40
                    ? Content.Substring(0, 40) + "..."
                    : Content;
    }
}
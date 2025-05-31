namespace Frontend.Models.Event
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Category { get; set; } = null!;
        public decimal Price { get; set; }
        public int MaxCapasity { get; set; }
        public DateTime EventDate { get; set; }
        public string Description { get; set; } = null!;
        public string? ImgUrl { get; set; }
        public string StatusId { get; set; } = null!;
        public string VenueId { get; set; } = null!;
        //public int SoldTickets { get; set; }
    }
}
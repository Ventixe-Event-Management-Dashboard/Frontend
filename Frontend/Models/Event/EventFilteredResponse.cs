namespace Frontend.Models.Event;

public class EventFilteredResponse
{
    public List<EventDto> Events { get; set; } = [];
    public int TotalCount { get; set; }
}

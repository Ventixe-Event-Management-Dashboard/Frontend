namespace Frontend.Models.Event;

public class EventViewModel
{
    public List<EventDto> Events { get; set; } = [];



    public int TotalEvents { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; } = 8; 


    public string? SearchQuery { get; set; }
    public string? SelectedCategory { get; set; }
    public string? SelectedTimeRange { get; set; }

  
    public bool IsGridView { get; set; } = true;

    public List<int> PageSizeOptions { get; set; } = new() { 8, 16 };
}
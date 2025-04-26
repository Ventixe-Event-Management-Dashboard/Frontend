using Frontend.Models;

namespace Frontend.ViewModels
{
    public class SidebarViewModel
    {
        public List<SidebarLink> Links { get; set; } = null!;
        public string? ActivePage { get; set; }
    }
}

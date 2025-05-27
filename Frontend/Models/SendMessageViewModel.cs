namespace Frontend.Models;

public class SendMessageViewModel
{
    public string? ReceiverId { get; set; }
    public string? Subject { get; set; }
    public string? Content { get; set; }

    public List<UserDto> Recipients { get; set; } = [];
}
public class UserDto
{
    public string Id { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public string FullName => $"{FirstName} {LastName}";
}

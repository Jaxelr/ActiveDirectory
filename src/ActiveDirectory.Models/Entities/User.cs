namespace ActiveDirectory.Models.Entities;

public class User
{
    public string UserName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Group { get; set; } = string.Empty;
}

namespace ActiveDirectory.Models.Operations;

public class AuthenticUserResponse
{
    public bool IsValid { get; set; }
    public string Message { get; set; } = string.Empty;
}

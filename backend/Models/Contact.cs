namespace backend.Models;

public class Contact
{
    public int Id { get; set; }
    public string ContactType { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Comments { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? GovernmentLevel { get; set; }
    public string? Industry { get; set; }
}
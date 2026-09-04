using backend.Enums;

namespace backend.DTOs;

public class CreateContactDto
{
    public ContactType ContactType { get; set; }

    public string Name { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Comments { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string? GovernmentLevel { get; set; }

    public string? Industry { get; set; }
}
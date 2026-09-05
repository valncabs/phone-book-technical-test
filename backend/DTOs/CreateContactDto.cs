using backend.Enums;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

public class CreateContactDto
{
    [Required(ErrorMessage = "Contact type is required.")]
    public ContactType ContactType { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [MinLength(2, ErrorMessage = "Name must contain at least 2 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required.")]
    [MinLength(2, ErrorMessage = "Last name must contain at least 2 characters.")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required.")]
    public string PhoneNumber { get; set; } = string.Empty;

    public string Comments { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string? Email { get; set; }

    public string? GovernmentLevel { get; set; }

    public string? Industry { get; set; }
}
using backend.Enums;
using System.ComponentModel.DataAnnotations;

namespace backend.DTOs;

/// <summary>
/// Data Transfer Object (DTO) for updating an existing contact.
/// Contains validation rules and allows modification of contact status.
/// </summary>
public class UpdateContactDto
{
    /// <summary>
    /// Type of contact (e.g., Person, PublicOrganization, PrivateOrganization).
    /// </summary>
    [Required(ErrorMessage = "Contact type is required.")]
    public ContactType ContactType { get; set; }

    /// <summary>
    /// First name of the contact.
    /// Must contain at least 2 characters.
    /// </summary>
    [Required(ErrorMessage = "Name is required.")]
    [MinLength(2, ErrorMessage = "Name must contain at least 2 characters.")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Last name of the contact (optional).
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Phone number of the contact.
    /// Required field.
    /// </summary>
    [Required(ErrorMessage = "Phone number is required.")]
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Additional comments or notes about the contact (optional).
    /// </summary>
    public string Comments { get; set; } = string.Empty;

    /// <summary>
    /// Email address of the contact.
    /// Must be in a valid email format if provided.
    /// </summary>
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string? Email { get; set; }

    /// <summary>
    /// Government level associated with the contact (optional).
    /// </summary>
    public string? GovernmentLevel { get; set; }

    /// <summary>
    /// Industry associated with the contact (optional).
    /// </summary>
    public string? Industry { get; set; }

    /// <summary>
    /// Current status of the contact (e.g., Active, Inactive).
    /// </summary>
    public ContactStatus Status { get; set; }
}

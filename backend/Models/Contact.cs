using backend.Enums;

namespace backend.Models;

/// <summary>
/// Represents a contact entity stored in the database.
/// Contains personal and organizational information along with status and timestamps.
/// </summary>
public class Contact
{
    /// <summary>
    /// Unique identifier for the contact.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Type of contact (e.g., Person, PublicOrganization, PrivateOrganization).
    /// </summary>
    public ContactType ContactType { get; set; }

    /// <summary>
    /// First name of the contact.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Last name of the contact.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Phone number of the contact.
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Additional comments or notes about the contact.
    /// </summary>
    public string Comments { get; set; } = string.Empty;

    /// <summary>
    /// Email address of the contact (optional).
    /// </summary>
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
    /// Current status of the contact (Active or Inactive).
    /// </summary>
    public ContactStatus Status { get; set; }

    /// <summary>
    /// Date and time when the contact was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Date and time when the contact was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}

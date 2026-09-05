namespace backend.Enums;

/// <summary>
/// Represents the type of a contact.
/// Used to categorize contacts into individuals or organizations.
/// </summary>
public enum ContactType
{
    /// <summary>
    /// A contact representing an individual person.
    /// </summary>
    Person,

    /// <summary>
    /// A contact representing a public organization
    /// (e.g., government agency, municipality).
    /// </summary>
    PublicOrganization,

    /// <summary>
    /// A contact representing a private organization
    /// (e.g., company, business, NGO).
    /// </summary>
    PrivateOrganization
}

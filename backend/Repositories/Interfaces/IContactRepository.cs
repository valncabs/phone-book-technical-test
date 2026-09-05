using backend.Models;

namespace backend.Repositories.Interfaces;

/// <summary>
/// Repository interface for managing contact entities.
/// Provides methods for creating, retrieving, updating, and soft-deleting contacts,
/// as well as checking for existing emails.
/// </summary>
public interface IContactRepository
{
    /// <summary>
    /// Creates a new contact in the database.
    /// </summary>
    /// <param name="contact">The contact entity to create.</param>
    /// <returns>The created contact.</returns>
    Task<Contact> CreateAsync(Contact contact);

    /// <summary>
    /// Retrieves all active contacts from the database.
    /// </summary>
    /// <returns>A list of contacts.</returns>
    Task<List<Contact>> GetAllAsync();

    /// <summary>
    /// Retrieves a contact by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the contact.</param>
    /// <returns>The contact if found, otherwise null.</returns>
    Task<Contact?> GetByIdAsync(int id);

    /// <summary>
    /// Updates an existing contact in the database.
    /// </summary>
    /// <param name="contact">The contact entity with updated information.</param>
    /// <returns>The updated contact.</returns>
    Task<Contact> UpdateAsync(Contact contact);

    /// <summary>
    /// Soft deletes a contact by its ID.
    /// Marks the contact as inactive instead of permanently removing it.
    /// </summary>
    /// <param name="id">The ID of the contact to delete.</param>
    /// <returns>True if the contact was successfully soft-deleted, false otherwise.</returns>
    Task<bool> SoftDeleteAsync(int id);

    /// <summary>
    /// Checks if a contact with the given email already exists.
    /// </summary>
    /// <param name="email">The email address to check.</param>
    /// <returns>True if the email exists, false otherwise.</returns>
    Task<bool> EmailExistsAsync(string email);
}

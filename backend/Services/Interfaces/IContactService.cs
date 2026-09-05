using backend.DTOs;
using backend.Models;

namespace backend.Services.Interfaces;

/// <summary>
/// Service interface for managing contacts.
/// Provides business logic methods for creating, retrieving, updating,
/// and soft-deleting contacts.
/// </summary>
public interface IContactService
{
    /// <summary>
    /// Retrieves all active contacts.
    /// </summary>
    /// <returns>A list of active contacts.</returns>
    Task<List<Contact>> GetAllAsync();

    /// <summary>
    /// Creates a new contact using the provided data transfer object.
    /// </summary>
    /// <param name="dto">The DTO containing contact information.</param>
    /// <returns>The created contact.</returns>
    Task<Contact> CreateAsync(CreateContactDto dto);

    /// <summary>
    /// Retrieves a contact by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the contact.</param>
    /// <returns>The contact if found, otherwise null.</returns>
    Task<Contact?> GetByIdAsync(int id);

    /// <summary>
    /// Updates an existing contact with new information.
    /// </summary>
    /// <param name="id">The ID of the contact to update.</param>
    /// <param name="dto">The DTO containing updated contact information.</param>
    /// <returns>The updated contact if found, otherwise null.</returns>
    Task<Contact?> UpdateAsync(int id, UpdateContactDto dto);

    /// <summary>
    /// Soft deletes a contact by marking its status as inactive.
    /// </summary>
    /// <param name="id">The ID of the contact to delete.</param>
    /// <returns>True if the contact was successfully soft-deleted, false otherwise.</returns>
    Task<bool> SoftDeleteAsync(int id);
}

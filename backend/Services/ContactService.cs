using backend.DTOs;
using backend.Enums;
using backend.Models;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services;

/// <summary>
/// Service implementation for managing contacts.
/// Provides business logic for creating, retrieving, updating,
/// and soft-deleting contacts.
/// </summary>
public class ContactService : IContactService
{
    private readonly IContactRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContactService"/> class.
    /// </summary>
    /// <param name="repository">The repository used for data access operations.</param>
    public ContactService(IContactRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Retrieves all active contacts.
    /// </summary>
    /// <returns>A list of active contacts.</returns>
    public async Task<List<Contact>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    /// <summary>
    /// Creates a new contact using the provided data transfer object.
    /// Validates email uniqueness before creation.
    /// </summary>
    /// <param name="dto">The DTO containing contact information.</param>
    /// <returns>The created contact.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if a contact with the same email already exists.
    /// </exception>
    public async Task<Contact> CreateAsync(CreateContactDto dto)
    {
        if (!string.IsNullOrWhiteSpace(dto.Email))
        {
            var emailExists = await _repository.EmailExistsAsync(dto.Email);

            if (emailExists)
            {
                throw new InvalidOperationException(
                    "A contact with this email already exists."
                );
            }
        }

        var contact = new Contact
        {
            ContactType = dto.ContactType,
            Name = dto.Name,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            Comments = dto.Comments,
            Email = dto.Email,
            GovernmentLevel = dto.GovernmentLevel,
            Industry = dto.Industry,

            Status = ContactStatus.Active,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        return await _repository.CreateAsync(contact);
    }

    /// <summary>
    /// Retrieves a contact by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the contact.</param>
    /// <returns>The contact if found, otherwise null.</returns>
    public async Task<Contact?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    /// <summary>
    /// Updates an existing contact with new information.
    /// </summary>
    /// <param name="id">The ID of the contact to update.</param>
    /// <param name="dto">The DTO containing updated contact information.</param>
    /// <returns>The updated contact if found, otherwise null.</returns>
    public async Task<Contact?> UpdateAsync(int id, UpdateContactDto dto)
    {
        var contact = await _repository.GetByIdAsync(id);

        if (contact == null)
        {
            return null;
        }

        contact.ContactType = dto.ContactType;
        contact.Name = dto.Name;
        contact.LastName = dto.LastName;
        contact.PhoneNumber = dto.PhoneNumber;
        contact.Comments = dto.Comments;
        contact.Email = dto.Email;
        contact.GovernmentLevel = dto.GovernmentLevel;
        contact.Industry = dto.Industry;
        contact.Status = dto.Status;

        contact.UpdatedAt = DateTime.UtcNow;

        return await _repository.UpdateAsync(contact);
    }

    /// <summary>
    /// Soft deletes a contact by marking its status as inactive.
    /// </summary>
    /// <param name="id">The ID of the contact to delete.</param>
    /// <returns>True if the contact was successfully soft-deleted, false otherwise.</returns>
    public async Task<bool> SoftDeleteAsync(int id)
    {
        return await _repository.SoftDeleteAsync(id);
    }
}

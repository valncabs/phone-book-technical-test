using backend.Data;
using backend.Models;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using backend.Enums;

namespace backend.Repositories;

/// <summary>
/// Repository implementation for managing contact entities.
/// Provides methods for creating, retrieving, updating, soft-deleting,
/// and validating contacts in the database.
/// </summary>
public class ContactRepository : IContactRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContactRepository"/> class.
    /// </summary>
    /// <param name="context">The application database context.</param>
    public ContactRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves all active contacts from the database.
    /// </summary>
    /// <returns>A list of active contacts ordered by ID.</returns>
    public async Task<List<Contact>> GetAllAsync()
    {
        return await _context.Contacts
            .Where(c => c.Status == ContactStatus.Active)
            .OrderBy(c => c.Id)
            .ToListAsync();
    }

    /// <summary>
    /// Creates a new contact in the database.
    /// </summary>
    /// <param name="contact">The contact entity to create.</param>
    /// <returns>The created contact.</returns>
    public async Task<Contact> CreateAsync(Contact contact)
    {
        await _context.Contacts.AddAsync(contact);
        await _context.SaveChangesAsync();
        return contact;
    }

    /// <summary>
    /// Retrieves a contact by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the contact.</param>
    /// <returns>The contact if found and active, otherwise null.</returns>
    public async Task<Contact?> GetByIdAsync(int id)
    {
        return await _context.Contacts
            .FirstOrDefaultAsync(c =>
                c.Id == id &&
                c.Status == ContactStatus.Active);
    }

    /// <summary>
    /// Updates an existing contact in the database.
    /// </summary>
    /// <param name="contact">The contact entity with updated information.</param>
    /// <returns>The updated contact.</returns>
    public async Task<Contact> UpdateAsync(Contact contact)
    {
        _context.Contacts.Update(contact);
        await _context.SaveChangesAsync();
        return contact;
    }

    /// <summary>
    /// Soft deletes a contact by marking its status as inactive.
    /// </summary>
    /// <param name="id">The ID of the contact to delete.</param>
    /// <returns>True if the contact was successfully soft-deleted, false otherwise.</returns>
    public async Task<bool> SoftDeleteAsync(int id)
    {
        var contact = await _context.Contacts
            .FirstOrDefaultAsync(c =>
                c.Id == id &&
                c.Status == ContactStatus.Active);

        if (contact == null)
        {
            return false;
        }

        contact.Status = ContactStatus.Inactive;
        await _context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Checks if an active contact with the given email already exists.
    /// </summary>
    /// <param name="email">The email address to check.</param>
    /// <returns>True if the email exists among active contacts, false otherwise.</returns>
    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Contacts
            .AnyAsync(c =>
                c.Email != null &&
                c.Email.ToLower() == email.ToLower() &&
                c.Status == ContactStatus.Active);
    }
}

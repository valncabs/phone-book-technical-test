using backend.Data;
using backend.Models;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using backend.Enums;

namespace backend.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly AppDbContext _context;

    public ContactRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Contact>> GetAllAsync()
    {
        return await _context.Contacts
            .Where(c => c.Status == ContactStatus.Active)
            .ToListAsync();
    }

    public async Task<Contact> CreateAsync(Contact contact)
    {
        await _context.Contacts.AddAsync(contact);
        await _context.SaveChangesAsync();
        return contact;
    }

    public async Task<Contact?> GetByIdAsync(int id)
    {
        return await _context.Contacts
            .FirstOrDefaultAsync(c =>
                c.Id == id &&
                c.Status == ContactStatus.Active);
    }

    public async Task<Contact> UpdateAsync(Contact contact)
    {
        _context.Contacts.Update(contact);
        await _context.SaveChangesAsync();
        return contact;
    }
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
}

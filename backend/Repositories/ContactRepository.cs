using backend.Data;
using backend.Models;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        return await _context.Contacts.ToListAsync();
    }
    public async Task<Contact> CreateAsync(Contact contact)
{
    await _context.Contacts.AddAsync(contact);

    await _context.SaveChangesAsync();

    return contact;
}
}
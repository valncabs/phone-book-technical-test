using backend.Models;

namespace backend.Repositories.Interfaces;

public interface IContactRepository
{
    Task<Contact> CreateAsync(Contact contact);
    Task<List<Contact>> GetAllAsync();
    Task<Contact?> GetByIdAsync(int id);
    Task<Contact> UpdateAsync(Contact contact);
    Task<bool> SoftDeleteAsync(int id);

    Task<bool> EmailExistsAsync(string email);
}
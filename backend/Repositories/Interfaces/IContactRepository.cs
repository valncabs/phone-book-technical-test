using backend.Models;

namespace backend.Repositories.Interfaces;

public interface IContactRepository
{
    Task<Contact> CreateAsync(Contact contact);
    Task<List<Contact>> GetAllAsync();
}
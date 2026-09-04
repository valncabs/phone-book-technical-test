using backend.DTOs;
using backend.Models;

namespace backend.Services.Interfaces;

public interface IContactService
{
    Task<List<Contact>> GetAllAsync();

    Task<Contact> CreateAsync(CreateContactDto dto);

    Task<Contact?> GetByIdAsync(int id);

    Task<Contact?> UpdateAsync(int id, UpdateContactDto dto);
}
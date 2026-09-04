using backend.Models;
using backend.DTOs;

namespace backend.Services.Interfaces;

public interface IContactService
{
    Task<List<Contact>> GetAllAsync();
    Task<Contact> CreateAsync(CreateContactDto dto);
}
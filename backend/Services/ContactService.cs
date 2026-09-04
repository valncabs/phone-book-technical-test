using backend.DTOs;
using backend.Enums;
using backend.Models;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services;

public class ContactService : IContactService
{
    private readonly IContactRepository _repository;

    public ContactService(IContactRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Contact>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Contact> CreateAsync(CreateContactDto dto)
    {
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

    public async Task<Contact?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

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
    public async Task<bool> SoftDeleteAsync(int id)
    {
        return await _repository.SoftDeleteAsync(id);
    }
}
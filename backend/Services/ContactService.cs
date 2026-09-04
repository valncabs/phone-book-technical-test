using backend.DTOs;
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
            PhoneNumber = dto.PhoneNumber,
            Comments = dto.Comments,
            Email = dto.Email,
            GovernmentLevel = dto.GovernmentLevel,
            Industry = dto.Industry
        };

        return await _repository.CreateAsync(contact);
    }
}
using backend.DTOs;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactService _service;

    public ContactController(IContactService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Contact>>> GetAll()
    {
        var contacts = await _service.GetAllAsync();

        return Ok(contacts);
    }

    [HttpPost]
    public async Task<ActionResult<Contact>> Create(CreateContactDto dto)
    {
        var contact = await _service.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetAll),
            new { id = contact.Id },
            contact
        );
    }
}


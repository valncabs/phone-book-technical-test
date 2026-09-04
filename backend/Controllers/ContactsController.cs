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

    // GET: api/contact
    [HttpGet]
    public async Task<ActionResult<List<Contact>>> GetAll()
    {
        var contacts = await _service.GetAllAsync();

        return Ok(contacts);
    }

    // GET: api/contact/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Contact>> GetById(int id)
    {
        var contact = await _service.GetByIdAsync(id);

        if (contact == null)
        {
            return NotFound();
        }

        return Ok(contact);
    }


    // POST: api/contact
    [HttpPost]
    public async Task<ActionResult<Contact>> Create(CreateContactDto dto)
    {
        var contact = await _service.CreateAsync(dto);

        return Ok(contact);
    }

    // PUT: api/contact/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult<Contact>> Update(
        int id,
        UpdateContactDto dto)
    {
        var contact = await _service.UpdateAsync(id, dto);

        if (contact == null)
        {
            return NotFound();
        }

        return Ok(contact);
    }
}
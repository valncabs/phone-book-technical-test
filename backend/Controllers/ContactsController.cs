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
            return NotFound(new
            {
                message = $"No se encontró un contacto activo con el ID {id}."
            });
        }

        return Ok(contact);
    }

    // POST: api/contact
    [HttpPost]
    public async Task<ActionResult<Contact>> Create(CreateContactDto dto)
    {
        var contact = await _service.CreateAsync(dto);

        return Ok(new
        {
            message = "El contacto fue creado correctamente.",
            data = contact
        });
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
            return NotFound(new
            {
                message = $"No se encontró un contacto activo con el ID {id}."
            });
        }

        return Ok(new
        {
            message = "El contacto fue actualizado correctamente.",
            data = contact
        });
    }

    // DELETE: api/contact/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.SoftDeleteAsync(id);

        if (!deleted)
        {
            return NotFound(new
            {
                message = $"No se encontró un contacto activo con el ID {id}. " + "Es posible que ya haya sido eliminado."
            });
        }

        return Ok(new
        {
            message = "El contacto fue eliminado correctamente."
        });
    }
}
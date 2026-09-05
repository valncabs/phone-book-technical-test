using backend.DTOs;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

/// <summary>
/// API Controller for managing contacts.
/// Provides endpoints to create, read, update, and delete contacts.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContactController"/>.
    /// </summary>
    /// <param name="service">Service layer for contact operations.</param>
    public ContactController(IContactService service)
    {
        _service = service;
    }

    /// <summary>
    /// Retrieves all active contacts.
    /// </summary>
    /// <returns>A list of contacts.</returns>
    /// <response code="200">Returns the list of contacts.</response>
    [HttpGet]
    public async Task<ActionResult<List<Contact>>> GetAll()
    {
        var contacts = await _service.GetAllAsync();
        return Ok(contacts);
    }

    /// <summary>
    /// Retrieves a contact by its ID.
    /// </summary>
    /// <param name="id">The ID of the contact.</param>
    /// <returns>The contact if found.</returns>
    /// <response code="200">Returns the contact.</response>
    /// <response code="404">If no active contact is found with the given ID.</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<Contact>> GetById(int id)
    {
        var contact = await _service.GetByIdAsync(id);

        if (contact == null)
        {
            return NotFound(new
            {
                message = $"No active contact found with ID {id}."
            });
        }

        return Ok(contact);
    }

    /// <summary>
    /// Creates a new contact.
    /// </summary>
    /// <param name="dto">The data transfer object containing contact information.</param>
    /// <returns>The created contact.</returns>
    /// <response code="200">Returns the created contact with a success message.</response>
    [HttpPost]
    public async Task<ActionResult<Contact>> Create(CreateContactDto dto)
    {
        var contact = await _service.CreateAsync(dto);

        return Ok(new
        {
            message = "The contact was created successfully.",
            data = contact
        });
    }

    /// <summary>
    /// Updates an existing contact.
    /// </summary>
    /// <param name="id">The ID of the contact to update.</param>
    /// <param name="dto">The data transfer object containing updated contact information.</param>
    /// <returns>The updated contact.</returns>
    /// <response code="200">Returns the updated contact with a success message.</response>
    /// <response code="404">If no active contact is found with the given ID.</response>
    [HttpPut("{id}")]
    public async Task<ActionResult<Contact>> Update(int id, UpdateContactDto dto)
    {
        var contact = await _service.UpdateAsync(id, dto);

        if (contact == null)
        {
            return NotFound(new
            {
                message = $"No active contact found with ID {id}."
            });
        }

        return Ok(new
        {
            message = "The contact was updated successfully.",
            data = contact
        });
    }

    /// <summary>
    /// Soft deletes a contact by its ID.
    /// </summary>
    /// <param name="id">The ID of the contact to delete.</param>
    /// <returns>Status of the deletion.</returns>
    /// <response code="200">Returns a success message if the contact was deleted.</response>
    /// <response code="404">If no active contact is found with the given ID.</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.SoftDeleteAsync(id);

        if (!deleted)
        {
            return NotFound(new
            {
                message = $"No active contact found with ID {id}. It may have already been deleted."
            });
        }

        return Ok(new
        {
            message = "The contact was deleted successfully."
        });
    }
}

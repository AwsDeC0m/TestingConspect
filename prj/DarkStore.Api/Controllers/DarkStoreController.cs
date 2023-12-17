using DarkStore.Api.Models;
using DarkStore.Api.Mappers;
using DarkStore.Api.Services;
using DarkStore.Api.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DarkStore.Api.Controllers;

[ApiController]
public class DarkStoreController : ControllerBase
{
    private readonly IDarkStoreService _darkStoreService;

    public DarkStoreController(IDarkStoreService darkStoreService)
    {
        _darkStoreService = darkStoreService;
    }

    [HttpGet("dark_stores")]
    public async Task<IActionResult> GetAll()
    {
        var stores = await _darkStoreService.GetAllAsync();
        var storesResponse = stores.Select(x => x.ToDarkStoreResponse());
        return Ok(storesResponse);
    }

    [HttpGet("dark_stores/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var store = await _darkStoreService.GetByIdAsync(id);
        if (store is null)
        {
            return NotFound();
        }

        var storeResponse = store.ToDarkStoreResponse();

        return Ok(storeResponse);
    }

    [HttpPost("dark_stores")]
    public async Task<IActionResult> Create([FromBody] DarkStoreCreateRequest createStoreRequest)
    {
        var store = new Models.DarkStore
        {
            FullName = createStoreRequest.FullName, 
            Number = createStoreRequest.Number
        };

        var created = await _darkStoreService.CreateAsync(store);
        if (!created)
        {
            // Implement validation
            return BadRequest();
        }

        var storeResponse = store.ToDarkStoreResponse();

        return CreatedAtAction(nameof(GetById), new {id = storeResponse.Id}, storeResponse);
    }

    [HttpDelete("dark_stores/{id:guid}")]
    public async Task<IActionResult> DeleteById(Guid id)
    {
        var deleted = await _darkStoreService.DeleteByIdAsync(id);
        if (!deleted)
        {
            return NotFound();
        }

        return Ok();
    }
}
